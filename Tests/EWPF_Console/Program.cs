using System;
using System.Collections.Concurrent;
using System.Threading;
namespace EWPF_Console 
{
    internal class Program
    {
        private static bool __ThreadUpdate = true;

        public static void Main(string[] args)
        {
            WebServerTest.Run();
            return;
            Thread.CurrentThread.Name = "Main thread";
            var clock_thread = new Thread(ThreadMethod);
            clock_thread.Name = "Second thread";
            clock_thread.IsBackground = true;
            clock_thread.Priority = ThreadPriority.BelowNormal;

            clock_thread.Start(42);  //first and bad method to start a thread


            //var count = 5;
            //var msg = "Hello World!";
            //var timeout = 150;

            ////second and good method to start a thread
            //new Thread(()=>PrintMethod(msg, count, timeout)) {IsBackground = true}.Start();

            //CheckThread();


            var values = new List<int>();

            var threads = new Thread[10];

            object lockObject=new object();

            for (var i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(() =>
                {
                    for (var j = 0; j < 10; j++)
                    {
                        lock (lockObject) 
                            values.Add(Thread.CurrentThread.ManagedThreadId);
                        Thread.Sleep(1);
                    }

                });
            }

            ////----------Can be used instead of lock()--------
            //Monitor.Enter(lockObject);
            //try
            //{
            //
            //}
            //finally
            //{
            //    Monitor.Exit(lockObject);
            //}
            ////------------------------------------------------


            foreach(var thread in threads)
                thread.Start();

            ////-----------Ways to stop threads (dangerous)-------------
            //if (!clock_thread.Join(100))
            //{
            //    //clock_thread.Abort(); //- Depricated: abort thread while every operation
            //    clock_thread.Interrupt();
            //}
            ////--------------------------------------------------------

            Console.ReadLine();
            Console.WriteLine(string.Join(",",values));
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

            while (__ThreadUpdate)
            {
                Thread.Sleep(100);
                Console.Title = DateTime.Now.ToString();
            }
        }

        /// <summary>
        /// Prints thread id into console
        /// </summary>
        private static void CheckThread()
        {
            var thread = Thread.CurrentThread;
            Console.WriteLine("Thread: "+thread.ManagedThreadId+" : "+thread.Name);
        }
    }
}