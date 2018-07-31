using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobs
{
    class Program
    {   
        static void Main(string[] args)
        {
            using (var job = new Job())
            {
                for (int i = 0; i < 5; i++)
                {
                    var p = Process.Start("notepad");
                    job.AddProcess(p);
                    Console.WriteLine($"Created notepad process.");
                }
                Console.WriteLine("Press ENTER to kil al Notepads.");
                Console.ReadKey();
                job.Kill();
            }

        }
    }
}
