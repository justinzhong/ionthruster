using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Ionthruster.Middleware.Build.Infrastructure
{
    public class ProcessRunner : IProcessRunner
    {
        private StringBuilder Output { get; }

        public event EventHandler<string> OnOutputReceived;

        public ProcessRunner()
        {
            Output = new StringBuilder();
        }

        public async Task<string> Run(string workingDirectory, string executable, params string[] parameters)
        {
            using (var process = new Process())
            {
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.FileName = executable;
                process.StartInfo.WorkingDirectory = workingDirectory;
                process.StartInfo.Arguments = string.Join(" ", parameters);
                process.OutputDataReceived += OutputDataReceived;
                process.Start();
                process.BeginOutputReadLine();

                await Task.Run(() => process.WaitForExit());
            }

            return Output.ToString();
        }

        private void OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Output.AppendLine(e.Data);

            var eventHandler = OnOutputReceived;

            if (eventHandler == null) return;

            eventHandler(sender, e.Data);
        }
    }
}
