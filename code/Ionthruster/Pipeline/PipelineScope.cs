using Ionthruster.Containers;
using Ionthruster.Tasks;
using System;
using System.Threading.Tasks;

namespace Ionthruster.Pipeline
{
    public class PipelineScope : IPipelineScope
    {
        private IComponentContainer Container { get; }
        private ITaskDelegateWrapper DelegateWrapper { get; }
        private bool Disposed { get; set; }

        public PipelineScope(IComponentContainer container, ITaskDelegateWrapper delegateWrapper)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (delegateWrapper == null) throw new ArgumentNullException(nameof(delegateWrapper));

            Container = container;
            DelegateWrapper = delegateWrapper;
        }

        ITaskPipeline IPipelineScope.Start<TTask>()
        {
            var taskDelegate = DelegateWrapper.Wrap(() => Container.Resolve<TTask>());

            return new TaskPipeline(Container, DelegateWrapper, taskDelegate);
        }

        public ITaskPipeline<TOutput> Start<TInput, TOutput>(TInput input, Func<IComponentContainer, IFuncTask<TInput, TOutput>> taskFactory)
        {
            var taskProvider = taskFactory(Container);
            var taskDelegate = DelegateWrapper.Wrap(input, () => taskProvider);

            return new TaskPipeline<TOutput>(Container, DelegateWrapper, taskDelegate);
        }

        async Task IPipelineScope.StartMiddleware<TMiddleware>()
        {
            var middleware = Container.Resolve<TMiddleware>();

            await middleware.Run(this);
        }

        public void Dispose()
        {
            if (Disposed) throw new ObjectDisposedException(nameof(PipelineScope));

            Container.Dispose();
            Disposed = true;
        }
    }
}