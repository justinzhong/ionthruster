using System;

namespace Ionthruster.Instrumentation
{
    public class LogWriter : ILogWriter
    {
        private Action<string> Writer { get; }

        public LogWriter(Action<string> writer)
        {
            if (writer == null) throw new ArgumentNullException(nameof(writer));

            Writer = writer;
        }

        public void Log(string message)
        {
            if (string.IsNullOrEmpty(message)) throw new ArgumentException("String cannot be empty or null", nameof(message));

            Writer(message);
        }
    }
}
