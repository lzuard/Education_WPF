using System;
using System.Runtime.CompilerServices;
using System.Threading;
using EducationWPF.Services.Interfaces;

namespace EducationWPF.Services
{
    internal class AsyncDataService : IAsyncDataService
    {
        private const int SleepTime = 7_000;
        public string GetResult(DateTime time)
        {
            Thread.Sleep(SleepTime);

            return $"Result value {time}";
        }
    }
}
