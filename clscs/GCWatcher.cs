// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace clscs
{
    internal static class GCWatcher
    {
        private readonly static ConditionalWeakTable<object, NotifyWhenGCd<string>> sCwt = new ConditionalWeakTable<object, NotifyWhenGCd<string>>();

        private sealed class NotifyWhenGCd<T>
        {
            private readonly T _value;

            internal NotifyWhenGCd(T value) =>
                _value = value;

            public override string ToString() => _value.ToString();

            ~NotifyWhenGCd() =>
                Console.WriteLine($"GC'd {_value}");
        }

        public static T GCWatch<T>(this T @object, string tag) where T :class
        {
            sCwt.Add(@object, new NotifyWhenGCd<string>(tag));
            return @object;
        }
    }
}
