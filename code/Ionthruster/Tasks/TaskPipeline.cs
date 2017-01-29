using Ionthruster.Containers;
using System;
using System.Threading.Tasks;

namespace Ionthruster.Tasks
{
    public class TaskPipeline<TOutput> : ITaskPipeline<TOutput>
    {
        private IComponentContainer Container { get; }
        private Func<Task<TOutput>> TaskDelegate { get; }

        public TaskPipeline(IComponentContainer container, Func<Task<TOutput>> taskDelegate)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (taskDelegate == null) throw new ArgumentNullException(nameof(taskDelegate));

            Container = container;
            TaskDelegate = taskDelegate;
        }

        ITaskPipeline<TNextOutput> ITaskPipeline<TOutput>.Branch<TNextOutput, TTask>()
        {
            Func<Task<TNextOutput>> taskDelegate = async () =>
            {
                await TaskDelegate(); // TODO: raise an event for logging etc.

                var task = Container.Resolve<TTask>();

                return await task.Run();
            };

            return new TaskPipeline<TNextOutput>(Container, taskDelegate);
        }

        public async Task<TOutput> Flush()
        {
            if (TaskDelegate == null) throw new ApplicationException("Pipeline is empty");

            return await TaskDelegate();
        }

        ITaskPipeline<TNextOutput> ITaskPipeline<TOutput>.Join<TTask, TNextOutput>()
        {
            Func<Task<TNextOutput>> taskDelegate = async () =>
            {
                var input = await TaskDelegate();
                var task = Container.Resolve<TTask>();

                return await task.Run(input);
            };

            return new TaskPipeline<TNextOutput>(Container, taskDelegate);
        }
    }
}