using System;
#if (MF_FRAMEWORK_VERSION_V4_2 || MF_FRAMEWORK_VERSION_V4_3)

#else
using System.Collections.Generic;
using System.Linq;
#endif
using System.Text;

namespace LifxLib
{
    public enum LifxPowerState
    {
        On = 1,
        Off = 0,
        Unknown = -1
    }
}
