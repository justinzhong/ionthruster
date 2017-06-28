using System;
using System.Threading.Tasks;
using Ionthruster.Containers;
using Ionthruster.Tasks;

namespace Ionthruster.Pipeline
{
    public interface ITaskDelegateWrapper
    {
        Func<Task> Wrap<TTask>(Func<TTask> taskProvider) where TTask : class, IActionTask;

        Func<Task<TOutput>> Wrap<TInput, TOutput>(TInput input, Func<IFuncTask<TInput, TOutput>> taskProvider);

        Func<Task<TOutput>> Wrap<TInput, TOutput>(Func<Task<TInput>> inputDelegate, Func<IFuncTask<TInput, TOutput>> taskProvider);
    }
}