using System;
using Autofac;
using Ionthruster.Middleware;

namespace Ionthruster.Modules
{
    public class MiddlewareModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .AssignableTo<IMiddleware>();
        }
    }
}