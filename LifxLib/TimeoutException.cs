using System;

namespace LifxLib
{
    class TimeoutException : Exception
    {
        private string p;

        public TimeoutException(string p)
        {
            // TODO: Complete member initialization
            this.p = p;
        }
    }
}
