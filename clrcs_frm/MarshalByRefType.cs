// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;
using System.Threading;

namespace clrcs_frm
{
    public sealed class MarshalByRefType : MarshalByRefObject
    {
        public MarshalByRefType() =>
            Console.WriteLine($"{GetType()} ctor running in {Thread.GetDomain().FriendlyName}");

        public void SomeMethod() =>
            Console.WriteLine($"Executing in {Thread.GetDomain().FriendlyName}");

        public MarshalByValType MethodWithReturn()
        {
            Console.WriteLine($"Executing in {Thread.GetDomain().FriendlyName}");
            var t = new MarshalByValType();
            return t;
        }

        public NonMarshalableType MethodArgAndReturn(string callingDomainName)
        {
            Console.WriteLine($"Calling from '{callingDomainName}' to '{Thread.GetDomain().FriendlyName}'");
            var t = new NonMarshalableType();
            return t;
        }
    }
}