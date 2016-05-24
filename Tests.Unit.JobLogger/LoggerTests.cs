using JobLogger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Unit.JobLogger
{
    [TestClass]
    public class LoggerTests
    {
        [TestMethod]
        public void Log_calls_listeners_with_correct_log_levels()
        {
            var mockListener1 = new Mock<ILogListener>();
            var mockListener2 = new Mock<ILogListener>();

            Logger logger = new Logger(LogLevel.Warning);
            logger.Listeners.Add(mockListener1.Object);
            logger.Listeners.Add(mockListener2.Object);


            // Assert
            logger.LogMessage("Should not log this");
            mockListener1.Verify(x => x.Log(It.IsAny<string>(), It.IsAny<LogLevel>()), Times.Never);
            mockListener2.Verify(x => x.Log(It.IsAny<string>(), It.IsAny<LogLevel>()), Times.Never);

            logger.LogWarning("This should be logged.");
            mockListener1.Verify(x => x.Log(It.Is<string>(s => s == "This should be logged."), It.IsAny<LogLevel>()), Times.Once);
            mockListener2.Verify(x => x.Log(It.Is<string>(s => s == "This should be logged."), It.IsAny<LogLevel>()), Times.Once);

            logger.LogError("This should be logged too.");
            mockListener1.Verify(x => x.Log(It.IsAny<string>(), It.IsAny<LogLevel>()), Times.Exactly(2));
            mockListener2.Verify(x => x.Log(It.IsAny<string>(), It.IsAny<LogLevel>()), Times.Exactly(2));
        }
    }
}
