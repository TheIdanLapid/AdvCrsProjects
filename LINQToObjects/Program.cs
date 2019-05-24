using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LINQToObjects
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintProcesses();
        }


        public static void PrintInterfaces()
        {
            Assembly assembly = typeof(string).Assembly;

            var list = from p in assembly.GetTypes()
                       where p.IsInterface
                       where p.IsPublic
                       orderby p.Name
                       select new
                       {
                           Name = p.Name,
                           NumOfMethods = p.GetMethods().Length
                       };

            foreach (var p in list)
            {
                Console.WriteLine(p);
            }
            Console.ReadKey();
        }

        public static void PrintProcesses()
        {
            var groups = from p in Process.GetProcesses()
                            where p.Threads.Count > 5
                            orderby p.Id
                            group new
                            {
                                Name = p.ProcessName,
                                PID = p.Id,
                                Priority = p.BasePriority
                            } by p.BasePriority;
            foreach (var processes in groups)
            {
                Console.WriteLine("Priority: " + processes.Key);
                foreach (var p in processes)
                {
                    Console.WriteLine(p);
                }
            }
            Console.ReadKey();
        }

        public static void CountAllThreads()
        {
            var threads = Process.GetProcesses().Sum(p => p.Threads.Count);
            Console.WriteLine($"There are {0} running", threads);
            Console.ReadKey();
        }
    }
}
