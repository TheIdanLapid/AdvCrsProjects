namespace Tasks
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            RunDemo();
        }
        
        public static void RunDemo() {
            var cts = new CancellationTokenSource();
            Task t = Task.Factory.StartNew(() => DoWork(cts.Token, 100));
            Console.WriteLine("Press ENTER to cancel operation");
            Console.ReadLine();
            cts.Cancel();
            try {
                t.Wait();
                Console.WriteLine("Task done.");
            }
            catch(AggregateException ex) {
                ex.Handle(e => e is OperationCanceledException);
                Console.WriteLine("Task cancelled.");
            }
        }
        private static void DoWork(CancellationToken ct, int data) {
            for(int i = 0; i < data; i++) {
                ct.ThrowIfCancellationRequested();
                // do some work...
                Console.WriteLine(i);
                Thread.Sleep(50); // waste some time
            }
        }
    }
}