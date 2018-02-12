using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class util
    {
        public static bool Is64BitProcess()
        {
            return IntPtr.Size == 8;
        }
    }
}
