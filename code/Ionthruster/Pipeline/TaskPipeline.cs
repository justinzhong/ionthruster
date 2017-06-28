using Ionthruster.Containers;
using Ionthruster.Tasks;
using System;
using System.Threading.Tasks;

namespace Ionthruster.Pipeline
{
    public class TaskPipeline : ITaskPipeline
    {
        private IComponentContainer Container { get; }
        private ITaskDelegateWrapper DelegateWrapper { get; }
        private Func<Task> TaskDelegate { get; }

        public TaskPipeline(IComponentContainer container, ITaskDelegateWrapper delegateWrapper, Func<Task> taskDelegate)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (delegateWrapper == null) throw new ArgumentNullException(nameof(delegateWrapper));
            if (taskDelegate == null) throw new ArgumentNullException(nameof(taskDelegate));

            Container = container;
            DelegateWrapper = delegateWrapper;
            TaskDelegate = taskDelegate;
        }

        public async Task Flush()
        {
            if (TaskDelegate == null) throw new ApplicationException("Pipeline is empty");

            await TaskDelegate();
        }

        ITaskPipeline ITaskPipeline.Join<TTask>()
        {
            Func<Task> taskDelegate = async () =>
            {
                await Flush();
                await DelegateWrapper.Wrap(() => Container.Resolve<TTask>())();
            };

            return new TaskPipeline(Container, DelegateWrapper, taskDelegate);
        }

        public ITaskPipeline<TOutput> Join<TInput, TOutput>(TInput input, Func<IComponentContainer, IFuncTask<TInput, TOutput>> taskProvider)
        {
            if (taskProvider == null) throw new ArgumentNullException(nameof(taskProvider));

            Func<Task<TOutput>> taskDelegate = async () =>
            {
                await Flush();

                return await DelegateWrapper.Wrap(input, () => taskProvider(Container))();
            };

            return new TaskPipeline<TOutput>(Container, DelegateWrapper, taskDelegate);
        }
    }
}