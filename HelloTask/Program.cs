using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HelloTaskApp
{
    class Program
    {
        const int _quantity = 10;

        static IEnumerable<Worker> Creator(int quantity, IPrinter printer)
        {
            for (int i = 0; i < quantity; i++)
            {
                yield return new Worker(i, printer);
            }
        }
        static void Main(string[] args)
        {
            IPrinter p = new Printer();

            p.Print("Press anykey to start workers...", y: _quantity, color: ConsoleColor.Yellow);
            Console.ReadKey();
            var workers = Creator(_quantity, p).ToArray();
            foreach (var w in workers)
            {
                w.Start();
            }
            p.Print("Workers is working, press anykey to stop them.", y: _quantity, color: ConsoleColor.Yellow);
            Console.ReadKey();
            foreach (var w in workers)
            {
                w.Stop();
            }
            p.Print("Workers is stopped, press anykey to exit program!", y: _quantity, color: ConsoleColor.Yellow);
            Console.ReadKey();
        }
    }
}
