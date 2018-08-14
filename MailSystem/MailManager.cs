using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSystem
{
    public class MailArrivedEventArgs : EventArgs
    {
        public readonly string _title;
        public readonly string _body;

        public MailArrivedEventArgs()
        {
            _title = "Dummy title";
            _body = "Dummy body";
        }

        public MailArrivedEventArgs(string title, string body)
        {
            _title = title;
            _body = body;
        }
    }

    public class MailManager
    {
        public event EventHandler<MailArrivedEventArgs> MailArrived;

        public MailManager()
        {

        }

        protected virtual void OnMailArrived(MailArrivedEventArgs e)
        {
            if (MailArrived != null)
            {
                MailArrived(this, e);
            }
        }

        public void SimulateMailArrived()
        {
            OnMailArrived(new MailArrivedEventArgs());
        }

        public void PrintTwice(object sender, MailArrivedEventArgs e)
        {
            string all = e._title + " " + e._body;
            Console.WriteLine("*" + all + "*");
        }
    }
}
