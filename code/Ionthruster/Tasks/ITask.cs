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
}