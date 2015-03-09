﻿using System;
#if (MF_FRAMEWORK_VERSION_V4_2 || MF_FRAMEWORK_VERSION_V4_3)

#else
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endif


namespace LifxLib.Messages
{
    public class LifxTagsMessage : LifxReceivedMessage
    {
        private const UInt16 PACKET_TYPE = 0x1C;

        public LifxTagsMessage()
            : base(PACKET_TYPE)
        {

        }

        public UInt64 Tags
        {
            get 
            {
                return BitConverter.ToUInt64(base.ReceivedData.Payload, 0);
            }
        }
    }
}
