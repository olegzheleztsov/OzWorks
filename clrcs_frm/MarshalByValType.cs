// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;
using System.Threading;

namespace clrcs_frm
{
    [Serializable]
    public class MarshalByValType 
    {
        private DateTime _creationTime = DateTime.Now;

        public MarshalByValType() =>
            Console.WriteLine(
                $"{GetType()} ctor running in {Thread.GetDomain().FriendlyName}, Created on {_creationTime:D}");

        public override string ToString() =>
            _creationTime.ToLongDateString();
    }
}