using System;
using System.Data.SqlClient;

namespace JobLogger
{
    /// <summary>
    /// A database log listener for SQL Server
    /// </summary>
    public class SqlLogListener : ILogListener
    {
        private string _connectionString;

        /// <summary>
        /// Creates a new DatabaseLogListener
        /// </summary>
        /// <param name="connectionString">SQL Server connection string</param>
        public SqlLogListener(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Logs a message with the specified log level to SQL Server
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="logLevel">Log level</param>
        public void Log(string message, LogLevel logLevel)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(_connectionString);
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "Insert into Log (DateTime, Message, LogLevel) Values(@dateTime, @message, @logLevel)";
                command.Parameters.Add(new SqlParameter("@dateTime", DateTime.Now));
                command.Parameters.Add(new SqlParameter("@message", message));
                command.Parameters.Add(new SqlParameter("@logLevel", (int)logLevel));

                command.ExecuteNonQuery();
            }
            catch
            {
                throw new ApplicationException("An error ocurred while writing a log to the database.");
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }
    }
}
