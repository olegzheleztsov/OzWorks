// Copyright (c) Zheleztsov Oleh. All Rights Reserved.
using System;
using System.Reflection;

namespace clrcs_frm
{
    public class DiscoverTypes
    {
        public static void Discover()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach(var a in assemblies)
            {
                Show(0, "Assembly: {0}", a);

                foreach(var t in a.ExportedTypes)
                {
                    Show(1, "Type: {0}", t);

                    foreach(var mi in t.GetTypeInfo().DeclaredMembers)
                    {
                        string typeName = string.Empty;
                        if(mi is Type)
                        {
                            typeName = "(Nested) Type";
                        }
                        if(mi is FieldInfo)
                        {
                            typeName = "FieldInfo";
                        }
                        if(mi is MethodInfo)
                        {
                            typeName = "MethodInfo";
                        }
                        if(mi is ConstructorInfo)
                        {
                            typeName = "ConstructorInfo";
                        }
                        if(mi is PropertyInfo)
                        {
                            typeName = "PropertyInfo";
                        }
                        if(mi is EventInfo)
                        {
                            typeName = "EventInfo";
                        }
                        Show(2, "{0}: {1}", typeName, mi);
                    }
                }
            }
        }

        private static void Show(int indent, string format, params object[] args) =>
            Console.WriteLine(new string(' ', 3 * indent) + format, args);
    }
}
