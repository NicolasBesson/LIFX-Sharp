using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace LifxLib
{
    
    public class UdpClient
    {

        private Socket _socket;
        private string _Hostname;
        private ushort _Port;
        private IPEndPoint _endPoint;
        private bool _Closed;

        AsyncCallback _DataCallback;
        Object _DataCallbackObject;
        ProcessClientRequest _processRequest;


        public UdpClient()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            EnableBroadcast = false;
            _Closed = false;
        }

        public UdpClient(IPEndPoint endPoint)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _Hostname = endPoint.Address.ToString();
            _Port = (ushort)endPoint.Port;
            _endPoint = endPoint;

            EnableBroadcast = false;
            _Closed = false;
        }

        public void Connect(IPEndPoint ip)
        {
            _Hostname = ip.Address.ToString();

            // Resolves the hostname to an IP address
            IPHostEntry address = Dns.GetHostEntry(_Hostname);
            // Creates the new IP end point
            EndPoint Destination = new IPEndPoint(address.AddressList[0], (int)ip.Port);
            // Connects to the socket
            _socket.Connect(Destination);
            this._Closed = false;
        }

        public void SetSocketOption(SocketOptionLevel socketOptionLevel, SocketOptionName socketOptionName, bool p)
        {
            _socket.SetSocketOption(socketOptionLevel, socketOptionName, p);
        }

        public void Send(byte[] buffer, int size)
        {
            _socket.Send(buffer, size, 0);

            // I implemented this sleep here for an issue I encountered.
            // For more details, read http://forums.netduino.com/index.php?/topic/4555-socket-error-10055-wsaenobufs/
            if (size < 32) Thread.Sleep(size * 10);
        }

        public void BeginReceive(AsyncCallback asyncCallback, Object state)
        {
            if (_Closed)
            {
                throw new InvalidOperationException();
            }

            _DataCallback = asyncCallback;
            _DataCallbackObject = state;
            _processRequest = new ProcessClientRequest(this, true);

        }

        internal byte[] EndReceive(IAsyncResult ar, ref IPEndPoint endPoint)
        {
            if (_Closed)
            {
                throw new InvalidOperationException();
            }

            _processRequest = null;
            return Data;
        }

        public void Close()
        {
            _Closed = true;
            _socket.Close();
        }

        public bool Blocking { get; set; }

        public bool EnableBroadcast { get; set; }

        public byte[] Data { get; set; }


        /// <summary>
        /// Processes a client request.
        /// </summary>
        internal sealed class ProcessClientRequest
        {
            private UdpClient _clientSocket;

            /// <summary>
            /// The constructor calls another method to handle the request, but can 
            /// optionally do so in a new thread.
            /// </summary>
            /// <param name="clientSocket"></param>
            /// <param name="asynchronously"></param>
            public ProcessClientRequest(UdpClient clientSocket, Boolean asynchronously)
            {
                _clientSocket = clientSocket;

                if (asynchronously)
                    // Spawn a new thread to handle the request.
                    new Thread(ProcessRequest).Start();
                else ProcessRequest();
            }

            /// <summary>
            /// Processes the request.
            /// </summary>
            private void ProcessRequest()
            {
                EndPoint endPoint = _clientSocket._endPoint;
                _clientSocket._socket.Bind(endPoint);

                while (true)
                {
                    if (_clientSocket._socket.Poll(-1, SelectMode.SelectRead))
                    {
                        byte[] buffer = new byte[_clientSocket._socket.Available];
                        int bytesRead = _clientSocket._socket.ReceiveFrom(buffer, ref endPoint);

                        // Store received data
                        _clientSocket.Data = buffer;

                        // Invoke call back
                        UDPAsyncResult ar = new UDPAsyncResult();
                        ar.AsyncState = (LifxCommunicator.UdpState)_clientSocket._DataCallbackObject;
                        _clientSocket._DataCallback.Invoke(ar);
                        break;
                    }
                    else
                    {
                        Thread.Sleep(10);
                    }
                }
            }
        }




    }
}

