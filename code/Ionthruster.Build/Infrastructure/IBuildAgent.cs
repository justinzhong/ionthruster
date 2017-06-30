using System.Threading.Tasks;

namespace Ionthruster.Build.Infrastructure
{
    public interface IBuildAgent
    {
        Task Build(string projectPath, params string[] args);
    }
}