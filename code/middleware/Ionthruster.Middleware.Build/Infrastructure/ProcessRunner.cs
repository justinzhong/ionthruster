using System.Diagnostics;
using System.Threading.Tasks;

namespace Ionthruster.Middleware.Build.Infrastructure
{
    public class ProcessRunner : IProcessRunner
    {
        public async Task<string> Run(string workingDirectory, string executable, params string[] parameters)
        {
            var process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.FileName = executable;
            process.StartInfo.WorkingDirectory = workingDirectory;
            process.StartInfo.Arguments = string.Join(" ", parameters);
            process.Start();

            var output = await Task.Run(() => process.StandardOutput.ReadToEnd());
            await Task.Run(() => process.WaitForExit());

            return output;
        }
    }
}
