using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Ionthruster.Middleware.Build.Infrastructure
{
    public class ProcessRunner : IProcessRunner
    {
        private StringBuilder Output { get; }

        private IPathDetector PathDetector { get; }

        public event EventHandler<string> OnOutputReceived;

        public ProcessRunner(IPathDetector pathDetector)
        {
            if (pathDetector == null) throw new ArgumentNullException(nameof(pathDetector));

            Output = new StringBuilder();
            PathDetector = pathDetector;
        }

        public async Task<string> Run(string workingDirectory, string executable, params string[] parameters)
        {
            ValidatePaths(workingDirectory, executable);

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

        private void ValidatePaths(string workingDirectory, string executable)
        {
            if (!PathDetector.DirectoryExists(workingDirectory)) throw new DirectoryNotFoundException($@"Directory {workingDirectory} could not be found");

            if (!PathDetector.FileExists(executable)) throw new FileNotFoundException($@"File {executable} could not be found");
        }
    }
}
