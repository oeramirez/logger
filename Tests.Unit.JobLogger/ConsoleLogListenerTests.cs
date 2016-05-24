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
    public class ConsoleLogListenerTests
    {
        [TestMethod]
        public void Log_logs_an_entry_to_console()
        {
            DateTime testTime = DateTime.Now;

            Logger logger = new Logger(LogLevel.Error);
            ConsoleLogListener listener = new ConsoleLogListener();
            listener.TestTime = testTime;
            logger.Listeners.Add(listener);

            string message = Guid.NewGuid().ToString();
            string expected = testTime.ToString("s") + " " + message;

            var actual = new StringWriter();
            Console.SetOut(actual);

            logger.LogError(message);

            Assert.IsTrue(actual.ToString().Contains(expected));
        }
    }
}
