using System;
using System.Threading.Tasks;

namespace Ionthruster.Middleware.Build.Infrastructure
{
    public interface IProcessRunner
    {
        event EventHandler<string> OnOutputReceived;

        Task<string> Run(string workingDirectory, string executable, params string[] parameters);
    }
}