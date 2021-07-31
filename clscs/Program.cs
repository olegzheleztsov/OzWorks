using System;
using System.Security.AccessControl;
using System.Threading;
using static System.Console;

namespace clscs
{
    class Program
    {
        static void Main(string[] args) =>
            BasicThreadSample();
            
            

        private static void BasicThreadSample()
        {
            WriteLine($"Starting dedicated thread to do an asynchronous operation");
            var dedicatedThread = new Thread(ComputeBoundOp);
            dedicatedThread.Start(5);
            
            WriteLine("Main thread: doing other work here...");
            Thread.Sleep(10000);
            dedicatedThread.Join();
            WriteLine("Hit <Enter> to end this program...");
            ReadLine();

            static void ComputeBoundOp(object state)
            {
                WriteLine($"In {nameof(ComputeBoundOp)}: state={state}");
                Thread.Sleep(1000);
            }
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
                WriteLine($"The byte[] did{(originalMemoryAddress == (IntPtr)pbytes ? " not " : null)} move during the GC");
                WriteLine($"Original address: {originalMemoryAddress}, current address: {(IntPtr)pbytes}");
            }
        }
    }
}
