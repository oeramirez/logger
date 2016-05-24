using JobLogger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Unit.JobLogger
{
    [TestClass]
    public class FileLogListenerTests
    {
        [TestMethod]
        public void Log_logs_an_entry_to_a_file()
        {
            DateTime testTime = DateTime.Now;

            Logger logger = new Logger(LogLevel.Message);
            FileLogListener listener = new FileLogListener(AppDomain.CurrentDomain.BaseDirectory);
            listener.TestTime = testTime;
            logger.Listeners.Add(listener);

            string message = Guid.NewGuid().ToString();
            string expectedText = string.Format("{0} {1} {2}", testTime.ToString("s"), "Warning", message);

            string expectedFileName = "LogFile_" + testTime.ToString("yyyy-MM-dd") + ".txt";
            string expectedFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, expectedFileName);

            logger.LogWarning(message);

            Assert.IsTrue(File.Exists(expectedFilePath));

            string actual = File.ReadAllText(expectedFilePath);
            Assert.IsTrue(actual.Contains(expectedText));
        }
    }
}
