using System;
using System.Collections;
using System.Collections.Generic;

namespace BoxingTest
{
    class Program
    {
        public class MyList
        {
            List<int> _list;

            public MyList() => _list = new List<int>();

            public void Add(int x)
            {
                int y = x;
                for(int i = 0; i < 10; i++)
                {
                    _list.Add(y);
                    y++;
                }
            }

            public void PrintItems()
            {
                foreach (int x in _list)
                    Console.Write("{0} ", x);

                Console.ReadKey();
            }
        }

        static void Main()
        {
            MyList m = new MyList();
            m.Add(1);
            m.PrintItems();
        }
    }
}
