using System;
using System.Threading.Tasks;
using Ionthruster.Tasks;

namespace Ionthruster.Pipeline
{
    public class TaskDelegateWrapper : ITaskDelegateWrapper
    {
        public Func<Task> Wrap<TTask>(Func<TTask> taskProvider)
            where TTask : class, IActionTask
        {
            Func<Task> taskDelegate = async () =>
            {
                var task = taskProvider();

                await task.Run();
            };

            return taskDelegate;
        }

        public Func<Task<TOutput>> Wrap<TInput, TOutput>(Func<Task<TInput>> inputDelegate, Func<IFuncTask<TInput, TOutput>> taskProvider)
        {
            Func<Task<TOutput>> taskDelegate = async () =>
            {
                var input = await inputDelegate();
                var task = taskProvider();

                return await task.Run(input);
            };

            return taskDelegate;
        }

        public Func<Task<TOutput>> Wrap<TInput, TOutput>(TInput input, Func<IFuncTask<TInput, TOutput>> taskProvider)
        {
            Func<Task<TOutput>> taskDelegate = async () =>
            {
                var task = taskProvider();

                return await task.Run(input);
            };

            return taskDelegate;
        }
    }
}