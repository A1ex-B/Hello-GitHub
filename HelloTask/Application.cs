using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloTaskApp
{
    public class Application
    {
        private readonly Config _config;
        private readonly IWorkerFactory _factory;
        private readonly IEnumerable<IWorker> _workers;
        private readonly IPrinter _printer;

        private IEnumerable<IWorker> MakeWorkers()
        {
            for (int i = 0; i < _config.quantity; i++)
            {
                yield return _factory.CreateWorker(i);
            }
        }

        public Application(Config config, IWorkerFactory factory, IPrinter printer)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _printer = printer ?? throw new ArgumentNullException(nameof(printer));
            _workers = MakeWorkers().ToArray();
        }

        public async Task RunAsync()
        {
            _printer.Print("Press anykey to start workers...", y: _config.quantity, color: ConsoleColor.Yellow);
            Console.ReadKey();
            foreach (var w in _workers)
            {
                w.Start();
            }
            _printer.Print("Workers are working, press anykey to stop them.", y: _config.quantity, color: ConsoleColor.Yellow);
            Console.ReadKey();

            var wt = _workers.Select(w => w.StopAsync()).ToArray();
            _printer.Print("Waiting to stop all workers...                 ", y: _config.quantity, color: ConsoleColor.Red);
            await Task.WhenAll(wt);
            _printer.Print("Workers are stopped, press anykey to exit program!", y: _config.quantity, color: ConsoleColor.Green);
            Console.ReadKey();
        }
    }
}
