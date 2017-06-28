using Ionthruster.Time;
using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Ionthruster.Instrumentation
{
    public class Logger : ILogger
    {
        private IDateTimeProvider Now { get; }
        private ILogWriter Writer { get; }

        public Logger(IDateTimeProvider now, ILogWriter writer)
        {
            if (now == null) throw new ArgumentNullException(nameof(now));
            if (writer == null) throw new ArgumentNullException(nameof(writer));

            Now = now;
            Writer = writer;
        }

        public async Task Log(string message,
            [CallerFilePath] string fileName = "")
        {
            var callerName = ExtractCallerName(fileName);
            var timestamp = GetTimestamp();

            await Task.Run(() =>
            {
                Writer.Log($"[{callerName}@{timestamp}] {message}");
            });
        }

        private string ExtractCallerName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return string.Empty;

            var callerName = (new FileInfo(fileName)).Name;

            return callerName.Contains('.') ? callerName.Split('.').First() : callerName;
        }

        private string GetTimestamp()
        {
            return Now.GetCurrentDateTime().ToString("yyyy-MM-dd hh:mm:ss");
        }
    }
}
