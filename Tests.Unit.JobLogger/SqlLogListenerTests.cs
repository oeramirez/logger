using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using JobLogger;
using System.Data.SqlClient;
using System.IO;

namespace Tests.Unit.JobLogger
{
    [TestClass]
    public class SqlLogListenerTests
    {
        [TestMethod]
        public void Log_logs_an_entry_to_the_database()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            path = Path.GetFullPath(Path.Combine(path, @"..\..\"));
            AppDomain.CurrentDomain.SetData("DataDirectory", path);

            string cn = ConfigurationManager.ConnectionStrings["LogsConnectionString"].ConnectionString;

            Logger logger = new Logger(LogLevel.Message);
            logger.Listeners.Add(new SqlLogListener(cn));

            string message = Guid.NewGuid().ToString();
            logger.LogMessage(message);

            int count = 0;

            using (SqlConnection con = new SqlConnection(cn))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM LOG WHERE Message = @message";
                cmd.Parameters.Add(new SqlParameter("@message", message));
                count = (int) cmd.ExecuteScalar();
            }

            Assert.AreEqual(1, count);
        }
    }
}
