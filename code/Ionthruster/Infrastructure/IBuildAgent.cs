using System.Threading.Tasks;

namespace Ionthruster.Infrastructure
{
    public interface IBuildAgent
    {
        Task Build(string projectPath, params string[] args);
    }
}