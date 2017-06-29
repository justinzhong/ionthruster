using System.Threading.Tasks;

namespace Ionthruster.Tasks
{
    public interface ITask
    {

    }

    public interface IVoidTask : ITask
    {
        Task Run();
    }

    public interface IFuncTask<in TInput, TOutput> : ITask
    {
        Task<TOutput> Run(TInput input);
    }
}