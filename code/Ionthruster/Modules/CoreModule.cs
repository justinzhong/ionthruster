using Autofac;
using Ionthruster.Instrumentation;
using System;

namespace Ionthruster.Modules
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<Func<DateTime>>(c => () => DateTime.Now);
            builder.Register<Action<string>>(c => message => Console.WriteLine(message));
            builder.RegisterType<Logger>().As<ILogger>();
        }
    }
}
