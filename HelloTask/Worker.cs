using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HelloTaskApp
{
    interface IWorker
    {
        void Start();
        void Stop();
    }

    class Worker : IWorker
    {
        private readonly IPrinter _printer;
        private int _id = -1;
        private CancellationTokenSource _cts;

        public Worker(int id, IPrinter printer)
        {
            _id = id;
            _printer = printer;
        }
        public override string ToString()
        {
            return $"Worker #{_id} with _cts:{_cts}";
        }
        public void Start()
        {
            this._cts = new CancellationTokenSource();
            Task.Run(
                async () =>
                {
                    _cts.Token.ThrowIfCancellationRequested();
                    while (true)
                    {
                        _printer.Print($"[{DateTime.Now:G}]:Worker #{_id} is working. Tread id {Thread.CurrentThread.ManagedThreadId:D5} ...", 0, _id, ConsoleColor.Magenta);
                        await Task.Delay(300);
                        _cts.Token.ThrowIfCancellationRequested();
                    }
                },
                _cts.Token);
            return;
        }
        public void Stop()
        {
            _cts.Cancel();
        }
    }
}
