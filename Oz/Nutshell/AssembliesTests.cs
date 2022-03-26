using System;
using System.Reflection;
using System.Runtime.Loader;

namespace Oz.Nutshell;

public class AssembliesTests
{
    public static void PrintAlc()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var context = AssemblyLoadContext.GetLoadContext(assembly);
        if (context != null)
        {
            Console.WriteLine(context.Name);
        }

        Console.WriteLine("***");
        foreach (var a in context.Assemblies)
        {
            Console.WriteLine(a.FullName);
        }
    }
}