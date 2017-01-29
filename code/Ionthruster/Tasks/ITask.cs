using System.Threading.Tasks;

namespace Ionthruster.Tasks
{
    public interface ITask<TOutput>
    {
        Task<TOutput> Run();
    }

    public interface ITask<in TInput, TOutput>
    {
        Task<TOutput> Run(TInput input);
    }
}