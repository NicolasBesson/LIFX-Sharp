using System;
#if (MF_FRAMEWORK_VERSION_V4_2 || MF_FRAMEWORK_VERSION_V4_3)

#else
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endif

namespace LifxLib.Messages
{
    public class LifxGetPowerStateCommand : LifxCommand
    {
        private const UInt16 PACKET_TYPE = 0x14;

        public LifxGetPowerStateCommand()
            : base(PACKET_TYPE, new LifxPowerStateMessage())
        {

        }

      
    }
}
