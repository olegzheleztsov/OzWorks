// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;
using System.Threading;

namespace clrcs_frm
{
    public class NonMarshalableType
    {
        public NonMarshalableType() =>
            Console.WriteLine($"Executing in {Thread.GetDomain().FriendlyName}");
    }
}