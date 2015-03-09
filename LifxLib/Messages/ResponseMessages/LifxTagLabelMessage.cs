using System;
#if (MF_FRAMEWORK_VERSION_V4_2 || MF_FRAMEWORK_VERSION_V4_3)

#else
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endif

namespace LifxLib.Messages
{
    public class LifxTagLabelMessage : LifxReceivedMessage
    {
        private const UInt16 PACKET_TYPE = 0x1F;

        public LifxTagLabelMessage()
            : base(PACKET_TYPE)
        {

        }

        public UInt64 Tag
        {
            get
            {
                return BitConverter.ToUInt64(base.ReceivedData.Payload, 0);
            }
        }

        public String TagLabel
        {
            get 
            {
#if (MF_FRAMEWORK_VERSION_V4_2 || MF_FRAMEWORK_VERSION_V4_3)
                char[] charMessage = System.Text.Encoding.UTF8.GetChars(base.ReceivedData.Payload);
                return new string(charMessage);
#else
                return Encoding.ASCII.GetString(base.ReceivedData.Payload, 8, 32);
#endif
            }
        }
    }
}
