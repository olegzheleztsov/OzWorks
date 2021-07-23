// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;

namespace clrcs_frm
{
    public class AppDomainMonitorDelta : IDisposable
    {
        private AppDomain _appDomain;
        private TimeSpan _thisADCpu;
        private long _thisADMemoryInUse;
        private long _thisADMemoryAllocated;

        static AppDomainMonitorDelta()
        {
            AppDomain.MonitoringIsEnabled = true;
        }

        public AppDomainMonitorDelta(AppDomain ad)
        {
            _appDomain = ad ?? AppDomain.CurrentDomain;
            _thisADCpu = _appDomain.MonitoringTotalProcessorTime;
            _thisADMemoryInUse = _appDomain.MonitoringSurvivedMemorySize;
            _thisADMemoryAllocated = _appDomain.MonitoringTotalAllocatedMemorySize;
        }

        public void Dispose()
        {
            GC.Collect();
            Console.WriteLine($"FriendlyName={_appDomain.FriendlyName}, CPU={(_appDomain.MonitoringTotalProcessorTime - _thisADCpu).TotalMilliseconds}ms");
            Console.WriteLine($"    Allocated {_appDomain.MonitoringTotalAllocatedMemorySize - _thisADMemoryAllocated:N0} bytes of which {_appDomain.MonitoringSurvivedMemorySize - _thisADMemoryInUse:N0} survived GCs");
        }
    }
}