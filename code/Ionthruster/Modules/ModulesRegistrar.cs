using Autofac;
using Ionthruster.Middleware;
using Ionthruster.Tasks;
using System;

namespace Ionthruster.Modules
{
    public static class ModulesRegistrar
    {
        public static void RegisterMiddlewares(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .AssignableTo<IMiddleware>();
        }

        public static void RegisterTasks(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .AssignableTo<IActionTask>();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .AsClosedTypesOf(typeof(IFuncTask<,>));
        }
    }
}