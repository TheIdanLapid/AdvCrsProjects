using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            MailManager m = new MailManager();
            m.MailArrived += (s, e) =>
            {
                Console.WriteLine(e._title + " " + e._body);
            };
            m.MailArrived += m.PrintTwice;
            m.SimulateMailArrived();

            Timer timer = new Timer(delegate
            {
                m.SimulateMailArrived();
            }, null, 1000, 1000);
            Console.ReadKey();
        }
    }
}
