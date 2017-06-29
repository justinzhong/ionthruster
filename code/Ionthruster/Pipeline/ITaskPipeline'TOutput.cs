using Ionthruster.Tasks;
using System;
using System.Threading.Tasks;

namespace Ionthruster.Pipeline
{
    public interface ITaskPipeline<TOutput>
    {
        Task<TOutput> Flush();

        ITaskPipeline Join<TTask>() where TTask : class, IActionTask;

        ITaskPipeline<TNextOutput> Join<TNextOutput>(Func<IFuncTask<TOutput, TNextOutput>> taskProvider);
    }
}