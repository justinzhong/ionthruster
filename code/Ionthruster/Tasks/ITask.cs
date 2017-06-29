using System;
using System.Threading.Tasks;

namespace Ionthruster.Tasks
{
    public interface ITask
    {

    }

    public interface IActionTask : ITask
    {
        Task Run();
    }

    public interface IFuncTask<TInput, TOutput> : ITask
    {
        Task<TOutput> Run(TInput input);
    }

    public interface IGlue
    {
        Task Flush();

        IGlue Join<IActionTask>();

        IGlue<TOutput> Join<TInput, TOutput>(Func<IFuncTask<TInput, TOutput>> taskProvider);
    }

    public interface IGlue<TOutput>
    {
        Task<TOutput> Flush();

        IGlue Join<IActionTask>();

        IGlue<TNextOutput> Join<TInput, TNextOutput>(Func<IFuncTask<TInput, TNextOutput>> taskProvider);
    }

    public interface ICleanProjectTask : IActionTask { }

    public interface IGitVersionTask : IFuncTask<string, long> { }

    public interface IBuildTask : IFuncTask<long, string> { }

    public interface ISomeTask : IFuncTask<int, int> { }

    public class Sandbox2
    {
        public void Play(IGlue glue)
        {
            glue.Join<ICleanProjectTask>()
                 .Join(() => default(IGitVersionTask))
                 .Join<ICleanProjectTask>()
                 .Join(() => default(IBuildTask))
                 .Join(() => default(IBuildTask))
        }
    }
}