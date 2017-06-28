using Ionthruster.Tasks;
using System;
using System.Threading.Tasks;

namespace Ionthruster.Pipeline
{
    public interface IPipelineScope : IDisposable
    {
        ITaskPipeline<string> Start<TTask>() where TTask : class, ITask<string>;

        ITaskPipeline<string> Start<TTask>(string arg) where TTask : class, ITask<string, string>;

        ITaskPipeline<string> Start<TInput, TTask>(TInput arg) where TTask : class, ITask<TInput, string>;

        ITaskPipeline<TOutput> Start<TTask, TOutput>() where TTask : class, ITask<TOutput>;

        ITaskPipeline<TOutput> Start<TInput, TTask, TOutput>(TInput arg) where TTask : class, ITask<TInput, TOutput>;

        Task StartMiddleware<TMiddleware>();
    }
}