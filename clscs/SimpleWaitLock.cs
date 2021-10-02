// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;
using System.Threading;

namespace clscs
{
    internal sealed class SimpleWaitLock : IDisposable
    {
        private readonly AutoResetEvent _available;

        public SimpleWaitLock() =>
            _available = new AutoResetEvent(true);

        public void Dispose() => _available.Dispose();

        public void Enter() =>
            _available.WaitOne();

        public void Leave() =>
            _available.Set();
    }
}