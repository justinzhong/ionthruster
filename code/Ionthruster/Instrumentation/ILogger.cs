using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Ionthruster.Instrumentation
{
    public interface ILogger
    {
        Task Log(string message, [CallerFilePath] string fileName = "");
    }
}