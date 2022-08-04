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

            thread.Start(42);  //first and bad method to start a thread


            var count = 5;
            var msg = "Hello World!";
            var timeout = 150;

            //second and good method to start a thread
            new Thread(()=>PrintMethod(msg, count, timeout)) {IsBackground = true}.Start();

            CheckThread();
            Console.ReadLine();
        }

        /// <summary>
        /// Thread method that can't be started with Thread.Start()
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="Count"></param>
        /// <param name="Timeout"></param>
        private static void PrintMethod(string Message, int Count, int Timeout)
        {
            for (var i = 0; i < Count; i++)
            {
                Console.WriteLine(Message);
                Thread.Sleep(Timeout);
            }
        }

        /// <summary>
        /// Thread method that can be started with Thread.Start();
        /// </summary>
        /// <param name="parameter"></param>
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