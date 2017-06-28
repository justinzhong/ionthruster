using Ionthruster.Containers;
using Ionthruster.Tasks;
using System;
using System.Threading.Tasks;

namespace Ionthruster.Pipeline
{
    public interface ITaskPipeline
    {
        Task Flush();

        ITaskPipeline Join<TTask>() where TTask : class, IActionTask;

        ITaskPipeline<TOutput> Join<TInput, TOutput>(TInput input, Func<IComponentContainer, IFuncTask<TInput, TOutput>> taskProvider);
    }
}