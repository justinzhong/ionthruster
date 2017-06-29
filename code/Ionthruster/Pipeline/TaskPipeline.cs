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
            await TaskDelegate();
        }

        ITaskPipeline ITaskPipeline.Join<TTask>()
        {
            var taskDelegate = DelegateWrapper.Wrap(() => Container.Resolve<TTask>());

            return new TaskPipeline(Container, DelegateWrapper, taskDelegate);
        }

        public ITaskPipeline<TOutput> Join<TInput, TOutput>(TInput input, Func<IComponentContainer, IFuncTask<TInput, TOutput>> taskProvider)
        {
            if (taskProvider == null) throw new ArgumentNullException(nameof(taskProvider));

            var taskDelegate = DelegateWrapper.Wrap(input, () => taskProvider(Container));

            return new TaskPipeline<TOutput>(Container, DelegateWrapper, taskDelegate);
        }
    }
}