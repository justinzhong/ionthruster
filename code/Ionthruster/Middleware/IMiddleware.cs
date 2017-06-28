using System.Threading.Tasks;
using Ionthruster.Pipeline;

namespace Ionthruster.Middleware
{
    public interface IMiddleware
    {
        Task Run(IPipelineScope scope);
    }
}