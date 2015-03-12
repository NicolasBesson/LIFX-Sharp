using System;
#if (MF_FRAMEWORK_VERSION_V4_2 || MF_FRAMEWORK_VERSION_V4_3)

#else
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endif

namespace LifxLib.Messages
{
    public class LifxSetTagsCommand : LifxCommand
    {
        private const UInt16 PACKET_TYPE = 0x1E;
        private UInt64 mTagsValue = 0;


        public LifxSetTagsCommand(UInt64 tagsValue)
            : base(PACKET_TYPE, null)
        {
            mTagsValue = tagsValue;
        }

        public override byte[] GetRawMessage()
        {
            return BitConverter.GetBytes(mTagsValue);
        }


        public UInt64 TagsValue
        {
            get { return mTagsValue; }
            set { mTagsValue = value; }
        }
    }
}
