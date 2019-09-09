using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace HelloTaskApp
{
    class MainModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Application>().AsSelf().SingleInstance();
            builder.RegisterType<Printer>().As<IPrinter>().SingleInstance();
            builder.RegisterType<Config>().AsSelf().SingleInstance();
            builder.RegisterType<RandomGenerator>().As<IRandomGenerator>().SingleInstance();

            builder.RegisterType<WorkersFactory>().As<IWorkerFactory>().InstancePerDependency();
            builder.RegisterType<Worker>().As<IWorker>().InstancePerDependency();
        }
    }
}
