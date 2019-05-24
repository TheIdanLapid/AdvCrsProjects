using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Delegates
{
    internal delegate void Feedback(int value);

    public sealed class Program
    {
        static void Main(string[] args)
        {
            StaticDelegateDemo();

        }

        private static void StaticDelegateDemo()
        {
            Console.WriteLine("---Static Delegate Demo---");
            Counter(1, 3, null);
            Counter(1, 3, new Feedback(Program.FeedbackToConsole));
            Counter(1, 3, new Feedback(FeedbackToMsgBox));
        }

        private static void InstanceDelegateDemo()
        {
            Console.WriteLine("---Instance Delegate Demo---");
            Program p = new Program();
            Counter(1, 3, new Feedback(p.FeedbackToFile));
        }

        private static void Counter(int from, int to, Feedback fb)
        {
            for (int val = from; val <= to; val++)
                if (fb != null)
                    fb(val);
        }
        

        private static void FeedbackToConsole(int value)
        {
            Console.WriteLine($"Item = {0}", value);
        }

        private static void FeedbackToMsgBox(int value)
        {
            MessageBox.Show("Item = " + value);
        }

        private void FeedbackToFile(int value)
        {
            StreamWriter sw = new StreamWriter("Status", true);
            sw.WriteLine($"Item = {0}", value);
            sw.Close();
        }


    }

}
