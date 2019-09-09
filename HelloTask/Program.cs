using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac;

namespace HelloTaskApp
{
    class Program
    {
        static async Task Main()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new MainModule());
            using (var container = builder.Build())
            {
                var application = container.Resolve<Application>();
                await application.RunAsync();
            }
        }
    }
}
