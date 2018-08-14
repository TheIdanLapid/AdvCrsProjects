using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
            CalcPrimes(from, to, thrds);
        }

        private static void CalcPrimes(int from, int to, int numofthreads)
        {
            ConcurrentBag<int> primes;
            primes = new ConcurrentBag<int>();
            List<Thread> threads = new List<Thread>();

            for (int t = 0; t < numofthreads; t++)
            {
                int delta = (to - from) / numofthreads;
                int start = delta * t;
                int end = to - delta;

                Thread thread = new Thread(() =>
                {
                    int cnt = 0;
                    for (int i = from; i < delta; i++)
                    {
                        if (IsPrime(i))
                        {
                            cnt++;
                            primes.Add(i);
                            Console.WriteLine("{0}'s prime. Thread #{1}", i, t);
                        }
                    }
                });
                threads.Add(thread);
            }
        }

        private static bool IsPrime(int number)
        {
            if (number < 2) return false;
            if (number % 2 == 0) return (number == 2);

            int root = (int) Math.Sqrt((double) number);

            for (int i = 3; i <= root; i += 2)
            {
                if (number % i == 0) return false;
            }

            return true;
        }
    }
}