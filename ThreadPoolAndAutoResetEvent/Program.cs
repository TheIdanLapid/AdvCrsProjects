using System;
using System.Threading;

namespace ThreadPoolAndAutoResetEvent
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var ev = new AutoResetEvent(false);
            ThreadPool.QueueUserWorkItem(data =>
            {
                Console.WriteLine($"ThreadPool thread {Thread.CurrentThread.ManagedThreadId}.");
                Thread.Sleep(3000);
                Console.WriteLine("ThreadPool thread done.");
                ev.Set();
            }, 7);
            Console.WriteLine("Main thread working...");
            ev.WaitOne(); // AutoResetEvent is live and false, waiting for it to become "true"
            Console.WriteLine("ALL DONE!");
        }
    }
}