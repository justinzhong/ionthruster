using System.Threading.Tasks;

namespace Ionthruster.Middleware.Build.Infrastructure
{
    public interface IBuildAgent
    {
        Task Build(string projectPath, params string[] args);
    }
}