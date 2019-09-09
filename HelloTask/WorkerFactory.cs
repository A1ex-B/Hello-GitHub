using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace HelloTaskApp
{
    public interface IWorkerFactory
    {
        IWorker CreateWorker(int id);
    }

    public class WorkersFactory : IWorkerFactory
    {
        private readonly ILifetimeScope _container;

        public WorkersFactory(ILifetimeScope container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public IWorker CreateWorker(int id)
        {
            return _container.Resolve<IWorker>(new NamedParameter("id", id));
        }
    }
}
