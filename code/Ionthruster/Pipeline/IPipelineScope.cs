using Ionthruster.Containers;
using Ionthruster.Middleware;
using Ionthruster.Tasks;
using System;
using System.Threading.Tasks;

namespace Ionthruster.Pipeline
{
    public interface IPipelineScope : IDisposable
    {
        ITaskPipeline Start<TTask>() where TTask : class, IActionTask;

        ITaskPipeline<TOutput> Start<TInput, TOutput>(TInput input, Func<IComponentContainer, IFuncTask<TInput, TOutput>> taskFactory);

        Task StartMiddleware<TMiddleware>() where TMiddleware : class, IMiddleware;
    }
}