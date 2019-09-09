using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HelloTaskApp
{
    public interface IWorker
    {
        void Start();
        Task Stop();
    }

    class Worker : IWorker
    {
        private readonly int _id;
        private readonly IPrinter _printer;
        private readonly IRandomGenerator _random;
        private Config _config;
        

        private CancellationTokenSource _cts;
        private Task _workerTask;

        public Worker(int id, IPrinter printer, IRandomGenerator random, Config config)
        {
            _id = id;
            _printer = printer ?? throw new ArgumentNullException(nameof(printer)); ;
            _random = random ?? throw new ArgumentNullException(nameof(random));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }
        public override string ToString()
        {
            return $"Worker #{_id} with _cts:{_cts}";
        }
        public void Start()
        {
            _cts = new CancellationTokenSource();
            _workerTask = Task.Run(
                async () =>
                {
                    _cts.Token.ThrowIfCancellationRequested();
                    while (true)
                    {
                        if (_id < _config.maxShow)
                        {
                            _printer.Print($"[{DateTime.Now:G}]:Hello from #{_id}! Tread id {Thread.CurrentThread.ManagedThreadId:D5} ...", 0, _id, ConsoleColor.Magenta);
                        }
                        await Task.Delay((int)(_random.GetRandom() * _config.maxWorkerDelay));
                        _cts.Token.ThrowIfCancellationRequested();
                    }
                },
                _cts.Token);
            return;
        }
        public Task Stop()
        {
            _cts.Cancel();
            return _workerTask;
        }
    }
}
