using System;
using System.IO;

namespace JobLogger
{
    /// <summary>
    /// A file log listener
    /// </summary>
    public class FileLogListener : ILogListener
    {
        /// <summary>
        /// Datetime for testing purposes only
        /// </summary>
        internal DateTime? TestTime;

        private string _logDirectory;

        /// <summary>
        /// Creates a new FileLogListener
        /// </summary>
        /// <param name="logDirectory">An existing directory where the log files will be stored.
        /// If the ddirectory doesn't exist an exception will be thrown.</param>
        public FileLogListener(string logDirectory)
        {
            if (!Directory.Exists(logDirectory))
                throw new ApplicationException("FileLogListener is invalid. Please specify an existing directory.");

            _logDirectory = logDirectory;
        }

        /// <summary>
        /// Logs a message with the specified log level to a file.
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="logLevel">Log level</param>
        /// <remarks>This logger creates a new file for every day. The name of the file uses thee sortable format 'LogFile_2016-05-23.txt'</remarks>
        public void Log(string message, LogLevel logLevel)
        {
            string filename = "LogFile_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

            string path = Path.Combine(_logDirectory, filename);

            string text = string.Format("{0} {1} {2}", (TestTime ?? DateTime.Now).ToString("s"), logLevel.ToString(), message);

            // AppendAllText creates the file if it doesn't exist
            File.AppendAllLines(path, new string[] { text });
        }
    }
}
