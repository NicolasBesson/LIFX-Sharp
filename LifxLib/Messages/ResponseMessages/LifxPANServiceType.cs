using System;
#if (MF_FRAMEWORK_VERSION_V4_2 || MF_FRAMEWORK_VERSION_V4_3)

#else
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endif

namespace LifxLib.Messages
{
    public enum LifxPANServiceType
    {
        UDP = (byte)0x01,
        TCP = (byte)0x02
    }
}
