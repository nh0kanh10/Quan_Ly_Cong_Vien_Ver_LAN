using System;
using System.Diagnostics;

namespace DAL
{
    internal static class DalErrorLogger
    {
        internal static void Log(string source, Exception ex)
        {
            Debug.WriteLine("[DAL][" + source + "] " + ex);
        }
    }
}
