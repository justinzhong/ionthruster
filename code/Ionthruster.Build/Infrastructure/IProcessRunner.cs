using System.Threading.Tasks;

namespace Ionthruster.Build.Infrastructure
{
    public interface IProcessRunner
    {
        Task<string> Run(string workingDirectory, string executable, params string[] parameters);
    }
}