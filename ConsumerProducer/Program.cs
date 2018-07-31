using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerProducer
{

    interface INotify
    {
        void Notify(object data);
    }

    class Producer
    {
        WeakReference _consumer;
        static int _data;

        public void Register(INotify consumer)
        {
            _consumer = new WeakReference(consumer);
        }

        public void DoSomething()
        {
            _data++;
            Console.WriteLine("Doing something. Data = {0}", _data);
            INotify not = (INotify)_consumer.Target;
            if (not != null)
                not.Notify(_data);
            else
                _consumer = null;
        }
    }

    class Consumer : INotify
    {
        public void Notify(object data)
        {
            Console.WriteLine("Recieved notification of data = {0}", data);
        }
    }
   
    class Program
    {
        static void Main(string[] args)
        {
            Producer p = new Producer();
            Consumer c = new Consumer();
            p.Register(c);

            p.DoSomething();
            c = null;
            p.DoSomething();
            GC.Collect();
            p.DoSomething();

            Console.ReadKey();
        }
    }
}
