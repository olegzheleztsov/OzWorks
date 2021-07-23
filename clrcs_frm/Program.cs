using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using static System.Console;
using static System.Environment;

namespace clrcs_frm
{
    internal class Program
    {
        public static void Main(string[] args) =>
            SingletonSerializationTest();

        private static void SingletonSerializationTest()
        {
            Singleton[] a1 = { Singleton.GetSingleton(), Singleton.GetSingleton() };
            WriteLine($"Do both elements refer to the same object? {(a1[0] == a1[1])}");

            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, a1);
                stream.Position = 0;
                Singleton[] a2 = (Singleton[])formatter.Deserialize(stream);
                WriteLine($"Do both elements refer to the same object? {(a2[0] == a2[1])}");
                WriteLine($"Do all elements refer to the same object? {(a1[0] == a2[0])}");
            }
        }

        private static void TestOpenTypes()
        {
            var openType = typeof(Dictionary<,>);
            var closedType = openType.MakeGenericType(typeof(string), typeof(int));
            var o = Activator.CreateInstance(closedType);
            WriteLine(o.GetType());
        }

        private static void TestAssemblyTypes()
        {
            var dataAssembly = "System.Data, version=4.0.0.0, " +
                               "culture=neutral, PublicKeyToken=b77a5c561934e089";
            LoadAssemAndShowPublicTypes(dataAssembly);
        }

        private static void LoadAssemAndShowPublicTypes(string assemId)
        {
            var a = Assembly.Load(assemId);
            foreach (var t in a.ExportedTypes)
            {
                WriteLine(t.FullName);
            }
        }

        private static void AppDomainResourceMonitoring()
        {
            using (new AppDomainMonitorDelta(null))
            {
                var list = new List<object>();
                for (var x = 0; x < 1000; x++)
                {
                    list.Add(new byte[10000]);
                }

                for (var x = 0; x < 2000; x++)
                {
                    new byte[10000].GetType();
                }

                long stop = TickCount + 5000;
                while (TickCount < stop)
                {
                    ;
                }
            }
        }

        private static void Marshalling()
        {
            var adCallingThreadDomain = Thread.GetDomain();

            var callingDomainName = adCallingThreadDomain.FriendlyName;
            WriteLine($"Default AppDomain's friendly name={callingDomainName}");

            var exeAssembly = Assembly.GetEntryAssembly().Location;
            WriteLine($"Main assembly={exeAssembly}");

            AppDomain ad2 = null;

            WriteLine($"{NewLine}Demo #1");
            ad2 = AppDomain.CreateDomain("AD #2", null, null);
            MarshalByRefType mbrt = null;

            mbrt = (MarshalByRefType)ad2.CreateInstanceFromAndUnwrap(exeAssembly,
                typeof(MarshalByRefType).FullName ?? string.Empty);
            WriteLine($"Type={mbrt.GetType()}");

            WriteLine($"Is proxy={RemotingServices.IsTransparentProxy(mbrt)}");
            mbrt.SomeMethod();
            AppDomain.Unload(ad2);

            try
            {
                mbrt.SomeMethod();
                WriteLine("Successful call");
            }
            catch (AppDomainUnloadedException)
            {
                WriteLine("Failed call.");
            }

            WriteLine($"{NewLine}Demo #2");
            ad2 = AppDomain.CreateDomain("AD #2", null, null);
            mbrt = (MarshalByRefType)ad2.CreateInstanceFromAndUnwrap(exeAssembly,
                typeof(MarshalByRefType).FullName ?? string.Empty);
            var mbvt = mbrt.MethodWithReturn();
            WriteLine($"Is proxy={RemotingServices.IsTransparentProxy(mbvt)}");
            WriteLine($"Returned object created: {mbvt}");
            AppDomain.Unload(ad2);

            try
            {
                WriteLine($"Returned object created {mbvt}");
                WriteLine("Successful call.");
            }
            catch (AppDomainUnloadedException)
            {
                WriteLine("Failed call.");
            }

            WriteLine($"{NewLine}Demo #3");
            ad2 = AppDomain.CreateDomain("AD #2", null, null);
            mbrt = (MarshalByRefType)ad2.CreateInstanceFromAndUnwrap(exeAssembly,
                typeof(MarshalByRefType).FullName ?? string.Empty);
            var nmt = mbrt.MethodArgAndReturn(callingDomainName);
        }
    }
}