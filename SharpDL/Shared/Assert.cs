using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL.Shared
{
    public static class Assert
    {
        public static void IsNotNull(IntPtr intPtr, string message)
        {
            Debug.Assert(intPtr != IntPtr.Zero, message);
        }

        public static void IsNotNull(Object anObject, string message)
        {
            Debug.Assert(anObject != null, message);
        }
    }
}
