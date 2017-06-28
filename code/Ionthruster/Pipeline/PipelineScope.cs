using Ionthruster.Containers;
using Ionthruster.Tasks;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Ionthruster.Pipeline
{
    public class PipelineScope : IPipelineScope
    {
        private IComponentContainer Container { get; }
        private bool Disposed { get; set; }

        public PipelineScope(IComponentContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));

            Container = container;
        }

        ITaskPipeline<string> IPipelineScope.Start<TTask>()
        {
            return BuildPipeline<TTask, string>();
        }

        ITaskPipeline<string> IPipelineScope.Start<TTask>(string arg)
        {
            return BuildPipeline<string, TTask, string>(arg);
        }

        ITaskPipeline<string> IPipelineScope.Start<TInput, TTask>(TInput arg)
        {
            return BuildPipeline<TInput, TTask, string>(arg);
        }

        ITaskPipeline<TOutput> IPipelineScope.Start<TTask, TOutput>()
        {
            return BuildPipeline<TTask, TOutput>();
        }

        ITaskPipeline<TOutput> IPipelineScope.Start<TInput, TTask, TOutput>(TInput arg)
        {
            return BuildPipeline<TInput, TTask, TOutput>(arg);
        }

        public void Dispose()
        {
            if (Disposed) throw new ObjectDisposedException(nameof(PipelineScope));

            Container.Dispose();
            Disposed = true;
        }

        private ITaskPipeline<TOutput> BuildPipeline<TTask, TOutput>()
           where TTask : class, ITask<TOutput>
        {
            Func<Task<TOutput>> taskDelegate = async () =>
            {
                var task = Container.Resolve<TTask>();

                return await task.Run();
            };

            return new TaskPipeline<TOutput>(Container, taskDelegate);
        }

        private ITaskPipeline<TOutput> BuildPipeline<TInput, TTask, TOutput>(TInput arg)
            where TTask : class, ITask<TInput, TOutput>
        {
            Func<Task<TOutput>> taskDelegate = async () =>
            {
                var task = Container.Resolve<TTask>();

                return await task.Run(arg);
            };

            return new TaskPipeline<TOutput>(Container, taskDelegate);
        }
    }
}