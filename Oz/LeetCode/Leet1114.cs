// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;
using System.Threading;

namespace Oz.LeetCode;

public class Leet1114
{
    public class Foo
    {

        private static AutoResetEvent _event1 = new AutoResetEvent(false);
        private static AutoResetEvent _event2 = new AutoResetEvent(false);
        public Foo() {
        
        }

        public void First(Action printFirst) {
        
            // printFirst() outputs "first". Do not change or remove this line.
            printFirst();
            _event1.Set();
        }

        public void Second(Action printSecond)
        {

            _event1.WaitOne();
            // printSecond() outputs "second". Do not change or remove this line.
            printSecond();
            _event2.Set();
        }

        public void Third(Action printThird)
        {

            _event2.WaitOne();
            // printThird() outputs "third". Do not change or remove this line.
            printThird();
        }
    }
}