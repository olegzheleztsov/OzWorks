using System;
using static System.Console;

namespace clscs
{
    class Program
    {
        static void Main(string[] args)
        {
            ConditionalWeakTableDemo();
        }

        private static void ConditionalWeakTableDemo()
        {
            var o = new object().GCWatch($"My object created at {DateTime.Now}");
            GC.Collect();
            GC.KeepAlive(o);
            o = null;

            GC.Collect();
            //ReadLine();
        }

        public static unsafe void Go()
        {
            for(var x = 0; x < 10000; x++)
            {
                new object();
            }

            IntPtr originalMemoryAddress;
            var bytes = new byte[1000];

            fixed(byte* pbytes = bytes)
            {
                originalMemoryAddress = (IntPtr)pbytes;
            }

            GC.Collect();

            fixed(byte* pbytes = bytes)
            {
                Console.WriteLine($"The byte[] did{(originalMemoryAddress == (IntPtr)pbytes ? " not " : null)} move during the GC");
                Console.WriteLine($"Original address: {originalMemoryAddress}, current address: {(IntPtr)pbytes}");
            }
        }
    }
}
