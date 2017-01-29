using System.Threading.Tasks;

namespace Ionthruster.Tasks
{
    public interface ITaskPipeline<TOutput>
    {
        ITaskPipeline<TNextOutput> Branch<TNextOutput, TTask>() where TTask : class, ITask<TNextOutput>;

        Task<TOutput> Flush();

        ITaskPipeline<TNextOutput> Join<TTask, TNextOutput>() where TTask : class, ITask<TOutput, TNextOutput>;
    }
}