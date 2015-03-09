using System;
#if (MF_FRAMEWORK_VERSION_V4_2 || MF_FRAMEWORK_VERSION_V4_3)

#else
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endif


namespace LifxLib.Messages
{
    public class LifxLabelMessage : LifxReceivedMessage
    {
        private const UInt16 PACKET_TYPE = 0x19;

        public LifxLabelMessage()
            : base(PACKET_TYPE)
        {

        }

        public String BulbLabel
        {
            get 
            {
#if (MF_FRAMEWORK_VERSION_V4_2 || MF_FRAMEWORK_VERSION_V4_3)
                char[] charMessage = System.Text.Encoding.UTF8.GetChars(base.ReceivedData.Payload);
                return new string(charMessage);
#else
                return Encoding.ASCII.GetString(base.ReceivedData.Payload);
#endif
             }
        }
    }
}
