using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadedPrimeCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            int primes, min;
            Stopwatch s = new Stopwatch();
            Dictionary<long, int> ThreadsToTimeDict = new Dictionary<long, int>();

            for (int i = 2; i < 15; i++)
            {
                s.Start();
                primes = CalcPrimes(3, 1000000, i);
                s.Stop();
                Console.WriteLine("With {0} threads is took {1}ms to find the {2} primes", i, s.ElapsedMilliseconds, primes);
                ThreadsToTimeDict[s.ElapsedMilliseconds] = i;
                s.Reset();
            }
            long bestTime = ThreadsToTimeDict.Keys.Min();
            Console.WriteLine("The best time is {0} with {1} threads", bestTime, ThreadsToTimeDict[bestTime]);
            Console.ReadKey();
        }

        public static int CalcPrimes(int from, int to, int numOfThreads)
        {
            ConcurrentQueue<int> primes = new ConcurrentQueue<int>();
            List<Thread> threads = new List<Thread>();
            int delta = (to - from) / numOfThreads;
            for (int i = 0; i < numOfThreads; i++)
            {
                int start = from + (delta * i), end = from + (delta * (i + 1));
                if (i == numOfThreads - 1) end = to;

                Thread t = new Thread(() => 
                {
                    int counter = 0;
                    for (int num = start; num < end; num++)
                    {
                        if (IsPrime(num))
                        {
                            counter++;
                            primes.Enqueue(num);
                        }
                    }
                    //Console.WriteLine("From {0} to {1} there are: {2}", start, end, counter);
                });
                threads.Add(t);
            }

            foreach (Thread t in threads)
                t.Start();

            foreach (Thread t in threads)
                t.Join();

            return primes.Count();

        }

        public static bool IsPrime(int num)
        {
            if (num <= 1) return false;
            if (num <= 3) return true;
            for (int i = 2; i <= Math.Sqrt(num); i++)
                if (num % i == 0)
                    return false;
            return true;
        }


    }
}
