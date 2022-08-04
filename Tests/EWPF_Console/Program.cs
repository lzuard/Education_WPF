using System;
using System.Threading;
namespace EWPF_Console 
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "Main thread";
            var thread = new Thread(ThreadMethod);
            thread.Name = "Second thread";
            thread.IsBackground = true;
            thread.Priority=ThreadPriority.BelowNormal;

            thread.Start(42);

            CheckThread();
            Console.ReadLine();
        }

        private static void ThreadMethod(object parameter)
        {
            var value = (int)parameter;
            Console.WriteLine(value);

            CheckThread();
        }

        private static void CheckThread()
        {
            var thread = Thread.CurrentThread;
            Console.WriteLine("Thread: "+thread.ManagedThreadId+" : "+thread.Name);
        }
    }
}