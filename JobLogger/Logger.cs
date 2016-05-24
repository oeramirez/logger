using System;
using System.Collections.Generic;

namespace JobLogger
{
    public class Logger
    {
        public List<ILogListener> Listeners
        {
            get { return _listeners; }
        }

        public LogLevel LogLevel
        {
            get { return _logLevel; }
            set { _logLevel = value; }
        }

        private readonly List<ILogListener> _listeners = new List<ILogListener>();
        private LogLevel _logLevel;

        public Logger(LogLevel logLevel)
        {
            _logLevel = logLevel;
        }

        public void LogMessage(string message)
        {
            Log(message, LogLevel.Message);
        }

        public void LogWarning(string message)
        {
            Log(message, LogLevel.Warning);
        }

        public void LogError(string message)
        {
            Log(message, LogLevel.Error);
        }

        public void Log(string message, LogLevel logLevel)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;

            if (logLevel < _logLevel)
                return;

            message = message.Trim();

            foreach (var listener in _listeners)
                listener.Log(message, logLevel);
        }
    }
}
