using System;

namespace JobLogger
{
    /// <summary>
    /// A console log listener
    /// </summary>
    public class ConsoleLogListener : ILogListener
    {
        /// <summary>
        /// Internal time for unit tests only
        /// </summary>
        internal DateTime? TestTime;

        /// <summary>
        /// Logs a message with the specified log level to the console
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="logLevel">Log level. Error causes the message to be printed in red, 
        /// Warning in yellow, an Message in white.</param>
        public void Log(string message, LogLevel logLevel)
        {
            if (logLevel == LogLevel.Error)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (logLevel == LogLevel.Warning)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.WriteLine((TestTime ?? DateTime.Now).ToString("s") + " " + message);
        }
    }
}
