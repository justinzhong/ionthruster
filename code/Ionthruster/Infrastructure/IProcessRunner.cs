using System.Threading.Tasks;

namespace Ionthruster.Infrastructure
{
    public interface IProcessRunner
    {
        Task<string> Run(string workingDirectory, string executable);
    }
}