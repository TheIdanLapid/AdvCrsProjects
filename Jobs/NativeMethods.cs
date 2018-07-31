using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Jobs
{
    public static class NativeMethods
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateJobObject(IntPtr JobAttributes, string lpName);

        [DllImport("kernel32.dll")]

        public static extern bool AssignProcessToJobObject(IntPtr job, IntPtr process);

        [DllImport("kernel32.dll")]
        public static extern void TerminateJobObject(IntPtr job, uint uExitCode);

        [DllImport("kernel32.dll")]
        public static extern void CloseHandle(IntPtr handle);
    }
}
