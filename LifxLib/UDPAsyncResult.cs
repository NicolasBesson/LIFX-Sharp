using System;
using Microsoft.SPOT;


namespace LifxLib
{
    class UDPAsyncResult : IAsyncResult
    {
        private LifxCommunicator.UdpState mstate;
        public UDPAsyncResult()
        {

        }

        public LifxCommunicator.UdpState AsyncState {
            get
            {
                return mstate;
            }

            set
            {
                mstate = value;

            }
        }
    }
}
