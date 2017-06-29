using Ionthruster.Containers;
using System;
using System.Threading.Tasks;

namespace Ionthruster.Pipeline
{
    public class TaskPipeline<TOutput> : ITaskPipeline<TOutput>
    {
        private IComponentContainer Container { get; }
        private ITaskDelegateWrapper DelegateWrapper { get; }
        private Func<Task<TOutput>> TaskDelegate { get; }

        public TaskPipeline(IComponentContainer container, ITaskDelegateWrapper delegateWrapper, Func<Task<TOutput>> taskDelegate)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (delegateWrapper == null) throw new ArgumentNullException(nameof(delegateWrapper));
            if (taskDelegate == null) throw new ArgumentNullException(nameof(taskDelegate));

            Container = container;
            DelegateWrapper = delegateWrapper;
            TaskDelegate = taskDelegate;
        }

        public async Task<TOutput> Flush()
        {
            if (TaskDelegate == null) throw new ApplicationException("Pipeline is empty");

            return await TaskDelegate();
        }

        ITaskPipeline ITaskPipeline<TOutput>.Join<TTask>()
        {
            var taskDelegate = DelegateWrapper.Wrap(() => Container.Resolve<TTask>());

            return new TaskPipeline(Container, DelegateWrapper, taskDelegate);
        }

        public ITaskPipeline<TNextOutput> Join<TNextOutput>(Func<Tasks.IFuncTask<TOutput, TNextOutput>> taskProvider)
        {
            if (taskProvider == null) throw new ArgumentNullException(nameof(taskProvider));

            var taskDelegate = DelegateWrapper.Wrap(async () => await Flush(), taskProvider);

            return new TaskPipeline<TNextOutput>(Container, DelegateWrapper, taskDelegate);
        }
    }
}