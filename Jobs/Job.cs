using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Jobs
{
    class Job : IDisposable
    {
        private IntPtr _handle;

        public Job(string name = null)
        {
            _handle = NativeMethods.CreateJobObject(IntPtr.Zero, name);
            if (IntPtr.Zero == _handle)
                throw new Win32Exception($"Failed to create job object {Marshal.GetLastWin32Error()}");
        }

        public void Add(IntPtr job, IntPtr process)
        {
            CheckIfValid();
            if (!NativeMethods.AssignProcessToJobObject(job, process))
                throw new Win32Exception("Failed to add process.");
        }

        public void AddProcess(Process process)
        {
            Add(_handle, process.Handle);
        }

        public void Kill(uint uExitCode = 0)
        {
            CheckIfValid();
            NativeMethods.TerminateJobObject(_handle, uExitCode);
        }
       

        public void Dispose(bool disposing)
        {
            NativeMethods.CloseHandle(_handle);
            _handle = IntPtr.Zero;
            if (disposing)
                GC.SuppressFinalize(this);

        }

        public void Dispose()
        {
            if (_handle == IntPtr.Zero)
                return;
            Dispose(false);
            
        }

        private void CheckIfValid()
        {
            if (_handle == IntPtr.Zero)
            {
                throw new ObjectDisposedException("_handle");
            }
        }

        ~Job()
        {
            Dispose(false);
        }


    }
}
