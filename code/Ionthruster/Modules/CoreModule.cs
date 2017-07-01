using Autofac;
using Ionthruster.Instrumentation;
using Ionthruster.Pipeline;
using Ionthruster.Time;
using System;

namespace Ionthruster.Modules
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<IDateTimeProvider>(_ => new DateTimeProvider(() => DateTime.Now));
            builder.Register<ILogWriter>(_ => new LogWriter(message => Console.WriteLine(message)));
            builder.RegisterType<Logger>().As<ILogger>();
            builder.RegisterType<TaskDelegateWrapper>().As<ITaskDelegateWrapper>();
        }
    }
}
