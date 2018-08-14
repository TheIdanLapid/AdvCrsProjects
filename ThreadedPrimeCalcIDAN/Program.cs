using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace ThreadedPrimeCalcIDAN
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Range?");
            int from = int.Parse(Console.ReadLine());
            int to = int.Parse(Console.ReadLine());
            Console.WriteLine("Threads?");
            int thrds = int.Parse(Console.ReadLine());
            
            Stopwatch s = Stopwatch.StartNew();
            Console.WriteLine(CalcPrimes(from, to, thrds));
            s.Stop();
            Console.WriteLine($"Time: {s.Elapsed}");
        }

        private static int CalcPrimes(int from, int to, int numofthreads)
        {
            ConcurrentQueue<int> primes = new ConcurrentQueue<int>();
            List<Thread> threads = new List<Thread>();
            int delta = (to - from) / numofthreads;

            for (int t = 0; t < numofthreads; t++)
            {
                int start = (delta * t) + from;
                int end = (t == numofthreads - 1) ? start + delta : to;

                Thread thread = new Thread(() =>
                {
                    for (int i = start; i < end; i++)
                    {
                        if (IsPrime(i))
                        {
                            primes.Enqueue(i);
                            Console.WriteLine("{0}'s prime. Thread #{1}", i, t);
                        }
                    }
                });
                thread.Start();
                threads.Add(thread);
            }

            foreach (Thread t in threads)
                t.Join();

            return primes.Count;
        }

        private static bool IsPrime(int number)
        {
            if (number < 2) return false;

            int root = (int) Math.Sqrt((double) number);

            for (int i = 2; i <= root; i++)
            {
                if (number % i == 0) return false;
            }

            return true;
        }
    }
}