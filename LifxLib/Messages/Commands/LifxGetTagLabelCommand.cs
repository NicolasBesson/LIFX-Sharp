﻿using System;
#if (MF_FRAMEWORK_VERSION_V4_2 || MF_FRAMEWORK_VERSION_V4_3)

#else
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endif

namespace LifxLib.Messages
{
    public class LifxGetTagLabelCommand : LifxCommand
    {
        private const UInt16 PACKET_TYPE = 0x1D;
        private UInt64 mTags = 0;


        public LifxGetTagLabelCommand(UInt64 tags)
            : base(PACKET_TYPE, new LifxTagLabelMessage())
        {
            mTags = tags;
        }

        #region ILifxMessage Members

        public override byte[] GetRawMessage()
        {
            return BitConverter.GetBytes(mTags);
        }

        #endregion

    }
}
