using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace FeedlyFeedProvider
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Feedly Feed Provider starting...");

            // Register COM class factory
            var classFactory = new FeedProviderClassFactory();
            var feedProviderGuid = new Guid("F3C06D85-4B8A-4E9C-9F1A-2D3E5C6B7A8D");
            uint cookie = 0;

            try
            {
                // Register the class factory
                int hr = NativeMethods.CoRegisterClassObject(
                    ref feedProviderGuid,
                    classFactory,
                    CLSCTX.CLSCTX_LOCAL_SERVER,
                    REGCLS.REGCLS_MULTIPLEUSE | REGCLS.REGCLS_SUSPENDED,
                    out cookie);

                if (hr < 0)
                {
                    Console.WriteLine($"Failed to register class factory: 0x{hr:X}");
                    return;
                }

                // Resume the registration
                hr = NativeMethods.CoResumeClassObjects();
                if (hr < 0)
                {
                    Console.WriteLine($"Failed to resume class objects: 0x{hr:X}");
                    return;
                }

                Console.WriteLine("Feed provider registered successfully. Press Ctrl+C to exit.");

                // Keep the application running
                var exitEvent = new ManualResetEvent(false);
                Console.CancelKeyPress += (sender, e) =>
                {
                    e.Cancel = true;
                    exitEvent.Set();
                };

                exitEvent.WaitOne();
            }
            finally
            {
                if (cookie != 0)
                {
                    NativeMethods.CoRevokeClassObject(cookie);
                }
                Console.WriteLine("Feed provider shutting down...");
            }
        }
    }

    [ComImport]
    [Guid("00000001-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IClassFactory
    {
        [PreserveSig]
        int CreateInstance(IntPtr pUnkOuter, ref Guid riid, out IntPtr ppvObject);

        [PreserveSig]
        int LockServer(bool fLock);
    }

    class FeedProviderClassFactory : IClassFactory
    {
        public int CreateInstance(IntPtr pUnkOuter, ref Guid riid, out IntPtr ppvObject)
        {
            ppvObject = IntPtr.Zero;

            if (pUnkOuter != IntPtr.Zero)
            {
                return unchecked((int)0x80040110); // CLASS_E_NOAGGREGATION
            }

            var feedProvider = new FeedProvider();
            var punk = Marshal.GetIUnknownForObject(feedProvider);

            try
            {
                int hr = Marshal.QueryInterface(punk, ref riid, out ppvObject);
                return hr;
            }
            finally
            {
                Marshal.Release(punk);
            }
        }

        public int LockServer(bool fLock)
        {
            return 0; // S_OK
        }
    }

    enum CLSCTX : uint
    {
        CLSCTX_INPROC_SERVER = 0x1,
        CLSCTX_INPROC_HANDLER = 0x2,
        CLSCTX_LOCAL_SERVER = 0x4,
        CLSCTX_REMOTE_SERVER = 0x10
    }

    [Flags]
    enum REGCLS : uint
    {
        REGCLS_SINGLEUSE = 0,
        REGCLS_MULTIPLEUSE = 1,
        REGCLS_MULTI_SEPARATE = 2,
        REGCLS_SUSPENDED = 4,
        REGCLS_SURROGATE = 8
    }

    static class NativeMethods
    {
        [DllImport("ole32.dll")]
        public static extern int CoRegisterClassObject(
            [In] ref Guid rclsid,
            [MarshalAs(UnmanagedType.IUnknown)] object pUnk,
            CLSCTX dwClsContext,
            REGCLS flags,
            out uint lpdwRegister);

        [DllImport("ole32.dll")]
        public static extern int CoRevokeClassObject(uint dwRegister);

        [DllImport("ole32.dll")]
        public static extern int CoResumeClassObjects();
    }
}
