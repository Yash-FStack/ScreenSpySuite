using EasyHook;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace ScreenSpyHook
{
    public class Main : IEntryPoint
    {
        // BitBlt signature:
        [DllImport("gdi32.dll", SetLastError = true)]
        static extern bool BitBlt(
            IntPtr hdcDest,
            int nXDest,
            int nYDest,
            int nWidth,
            int nHeight,
            IntPtr hdcSrc,
            int nXSrc,
            int nYSrc,
            uint dwRop);

        // Delegate matching BitBlt signature
        [UnmanagedFunctionPointer(CallingConvention.StdCall,
            SetLastError = true)]
        delegate bool BitBltDelegate(
            IntPtr hdcDest,
            int nXDest,
            int nYDest,
            int nWidth,
            int nHeight,
            IntPtr hdcSrc,
            int nXSrc,
            int nYSrc,
            uint dwRop);

        LocalHook bitBltHook;
        public Main(RemoteHooking.IContext InContext, string InChannelName)
        {
            // Hook BitBlt inside target process
            IntPtr bitBltAddress = LocalHook.GetProcAddress("gdi32.dll", "BitBlt");
            bitBltHook = LocalHook.Create(
                bitBltAddress,
                new BitBltDelegate(BitBlt_Hooked),
                this);

            bitBltHook.ThreadACL.SetExclusiveACL(new int[] { 0 }); // hook all threads

            // Keep the thread alive
            ThreadPool.QueueUserWorkItem(state =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                }
            });
        }

        // The hook function
        bool BitBlt_Hooked(
            IntPtr hdcDest,
            int nXDest,
            int nYDest,
            int nWidth,
            int nHeight,
            IntPtr hdcSrc,
            int nXSrc,
            int nYSrc,
            uint dwRop)
        {
            // Log or show a message when screen capture happens
            string message = $"BitBlt called in PID {Process.GetCurrentProcess().Id}";

            // For demo: popup a message box (you can log instead)
            // MessageBox.Show(message); // Avoid UI in hook, or it will freeze

            Console.WriteLine(message); // Logs to target process console (if any)

            // Call original BitBlt function
            return BitBlt(
                hdcDest,
                nXDest,
                nYDest,
                nWidth,
                nHeight,
                hdcSrc,
                nXSrc,
                nYSrc,
                dwRop);
        }
    }
}
