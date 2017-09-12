using System;
using System.Linq;
using Iris.Logging.Logic.Logger;
using Iris.Logging.Logic.Models;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Repository.Hierarchy;
using NUnit.Framework;

namespace Iris.Logging.UnitTests
{
    [TestFixture]
    public class LogWrapperTests
    {
        private MemoryAppender _appender;
        private const string LogMessage = "test message";
        private readonly Exception _exception = new Exception("test exception");

        #region Debug

        [Test]
        public void WhenDebugEnabled_ThenLogDebugMessages()
        {
            var logManager = GetLogManager(Level.Debug);
            logManager.Info(new IntegrationLogModel() {Message = LogMessage});

            Assert.AreEqual(LogMessage, GetLogMessage());
        }

        [Test]
        public void WhenDebugEnabled_ThenLogDebugMessagesWithException()
        {
            var logManager = GetLogManager(Level.Debug);
            logManager.Info(new IntegrationLogModel() {Message = LogMessage, Exception = _exception});

            Assert.AreEqual(LogMessage, GetLogMessage());
            Assert.AreEqual(GetLogException(), GetLogExceptionMessage());
        }

        [Test]
        public void WhenDebugDisabled_ThenNotLogDebugMessages()
        {
            var logManager = GetLogManager(Level.Info);
            logManager.Debug(new IntegrationLogModel() {Message = LogMessage});

            Assert.IsNull(GetLogMessage());
        }

        #endregion

        #region Info

        [Test]
        public void WhenInfoEnabled_ThenLogInfoMessages()
        {
            var logManager = GetLogManager(Level.Info);
            logManager.Info(new IntegrationLogModel() {Message = LogMessage});

            Assert.AreEqual(LogMessage, GetLogMessage());
        }

        [Test]
        public void WhenInfoEnabled_ThenLogInfoMessagesWithException()
        {
            var logManager = GetLogManager(Level.Info);
            logManager.Info(new IntegrationLogModel() {Message = LogMessage, Exception = _exception});

            Assert.AreEqual(LogMessage, GetLogMessage());
            Assert.AreEqual(GetLogException(), GetLogExceptionMessage());
        }

        [Test]
        public void WhenInfoDisabled_ThenNotLogInfoMessages()
        {
            var logManager = GetLogManager(Level.Warn);
            logManager.Info(new IntegrationLogModel() {Message = LogMessage});

            Assert.IsNull(GetLogMessage());
        }

        #endregion

        #region Error

        [Test]
        public void WhenErrorEnabled_ThenLogErrorMessages()
        {
            var logManager = GetLogManager(Level.Error);
            logManager.Error(new IntegrationLogModel() {Message = LogMessage});

            Assert.AreEqual(LogMessage, GetLogMessage());
        }

        [Test]
        public void WhenErrorEnabled_ThenLogErrorMessagesWithException()
        {
            var logManager = GetLogManager(Level.Error);
            logManager.Error(new IntegrationLogModel() {Message = LogMessage, Exception = _exception});

            Assert.AreEqual(LogMessage, GetLogMessage());
            Assert.AreEqual(GetLogException(), GetLogExceptionMessage());
        }

        [Test]
        public void WhenErrorDisabled_ThenNotLogErrorMessages()
        {
            var logManager = GetLogManager(Level.Fatal);
            logManager.Error(new IntegrationLogModel() {Message = LogMessage});

            Assert.IsNull(GetLogMessage());
        }

        #endregion

        #region Warn

        [Test]
        public void WhenWarnEnabled_ThenLogWarnMessages()
        {
            var logManager = GetLogManager(Level.Warn);
            logManager.Warn(new IntegrationLogModel() {Message = LogMessage});

            Assert.AreEqual(LogMessage, GetLogMessage());
        }

        [Test]
        public void WhenWarnEnabled_ThenLogWarnMessagesWithException()
        {
            var logManager = GetLogManager(Level.Warn);
            logManager.Warn(new IntegrationLogModel() {Message = LogMessage, Exception = _exception});

            Assert.AreEqual(LogMessage, GetLogMessage());
            Assert.AreEqual(GetLogException(), GetLogExceptionMessage());
        }

        [Test]
        public void WhenWarnDisabled_ThenNotLogWarnMessages()
        {
            var logManager = GetLogManager(Level.Error);
            logManager.Warn(new IntegrationLogModel() {Message = LogMessage});

            Assert.IsNull(GetLogMessage());
        }

        #endregion

        #region Fatal

        [Test]
        public void WhenFatalEnabled_ThenLogFatalMessages()
        {
            var logManager = GetLogManager(Level.Fatal);
            logManager.Fatal(new IntegrationLogModel() {Message = LogMessage});

            Assert.AreEqual(LogMessage, GetLogMessage());
        }

        [Test]
        public void WhenFatalEnabled_ThenLogFatalMessagesWithException()
        {
            var logManager = GetLogManager(Level.Fatal);
            logManager.Fatal(new IntegrationLogModel() {Message = LogMessage, Exception = _exception});

            Assert.AreEqual(LogMessage, GetLogMessage());
            Assert.AreEqual(GetLogException(), GetLogExceptionMessage());

        }

        [Test]
        public void WhenOff_ThenNotLogFatalMessages()
        {
            var logManager = GetLogManager(Level.Off);
            logManager.Warn(new IntegrationLogModel() {Message = LogMessage});

            Assert.IsNull(GetLogMessage());
        }

        #endregion

        #region Complex

        [Test]
        public void WhenAllEnabled_ThenLogAllLevelMessages()
        {
            var logManager = GetLogManager(Level.All);
            LogAllLevelLogs(logManager);

            Assert.AreEqual(5, GetAllEvents().ToList().Count);
            Assert.AreEqual(string.Concat(LogMessage, Level.Debug), GetLogMessage(0));
            Assert.AreEqual(string.Concat(LogMessage, Level.Info), GetLogMessage(1));
            Assert.AreEqual(string.Concat(LogMessage, Level.Warn), GetLogMessage(2));
            Assert.AreEqual(string.Concat(LogMessage, Level.Error), GetLogMessage(3));
            Assert.AreEqual(string.Concat(LogMessage, Level.Fatal), GetLogMessage(4));
        }

        [Test]
        public void WhenDebugEnabled_ThenLogAllLevels()
        {
            var logManager = GetLogManager(Level.Debug);

            LogAllLevelLogs(logManager);

            Assert.AreEqual(5, GetAllEvents().ToList().Count);
            Assert.AreEqual(string.Concat(LogMessage, Level.Debug), GetLogMessage(0));
            Assert.AreEqual(string.Concat(LogMessage, Level.Info), GetLogMessage(1));
            Assert.AreEqual(string.Concat(LogMessage, Level.Warn), GetLogMessage(2));
            Assert.AreEqual(string.Concat(LogMessage, Level.Error), GetLogMessage(3));
            Assert.AreEqual(string.Concat(LogMessage, Level.Fatal), GetLogMessage(4));
        }

        [Test]
        public void WhenInfoEnabled_ThenLogInfoAndSubsequent()
        {
            var logManager = GetLogManager(Level.Info);

            LogAllLevelLogs(logManager);

            Assert.AreEqual(4, GetAllEvents().ToList().Count);
            Assert.AreEqual(string.Concat(LogMessage, Level.Info), GetLogMessage(0));
            Assert.AreEqual(string.Concat(LogMessage, Level.Warn), GetLogMessage(1));
            Assert.AreEqual(string.Concat(LogMessage, Level.Error), GetLogMessage(2));
            Assert.AreEqual(string.Concat(LogMessage, Level.Fatal), GetLogMessage(3));
        }

        [Test]
        public void WhenWarnEnabled_ThenLogWarnAndSubsequent()
        {
            var logManager = GetLogManager(Level.Warn);

            LogAllLevelLogs(logManager);

            Assert.AreEqual(3, GetAllEvents().ToList().Count);
            Assert.AreEqual(string.Concat(LogMessage, Level.Warn), GetLogMessage(0));
            Assert.AreEqual(string.Concat(LogMessage, Level.Error), GetLogMessage(1));
            Assert.AreEqual(string.Concat(LogMessage, Level.Fatal), GetLogMessage(2));
        }

        [Test]
        public void WhenErrorEnabled_ThenLogErrorAndSubsequent()
        {
            var logManager = GetLogManager(Level.Error);

            LogAllLevelLogs(logManager);

            Assert.AreEqual(2, GetAllEvents().ToList().Count);
            Assert.AreEqual(string.Concat(LogMessage, Level.Error), GetLogMessage(0));
            Assert.AreEqual(string.Concat(LogMessage, Level.Fatal), GetLogMessage(1));
        }

        [Test]
        public void WhenFatalEnabled_ThenLogErrorAndSubsequent()
        {
            var logManager = GetLogManager(Level.Fatal);

            LogAllLevelLogs(logManager);

            Assert.AreEqual(1, GetAllEvents().ToList().Count);
            Assert.AreEqual(string.Concat(LogMessage, Level.Fatal), GetLogMessage(0));
        }

        [Test]
        public void WhenOff_ThenNoLogs()
        {
            var logManager = GetLogManager(Level.Off);
            LogAllLevelLogs(logManager);

            Assert.IsNull(GetAllEvents());
        }

        #endregion

        #region Private

        private ILogWrapper GetLogManager(Level level)
        {
            _appender = new MemoryAppender
            {
                Name = "Unit Testing Appender",
                Layout = new log4net.Layout.PatternLayout("%message"),
                Threshold = level
            };
            _appender.ActivateOptions();

            //var root = ((Hierarchy)LogManager.GetRepository()).Root;
            //root.AddAppender(_appender);
            //root.Repository.Configured = true;

            //var rootLogger = LogManager.GetLogger("root");

            LogWrapper logger = new LogWrapper(_appender, level.ToString());
            logger.Initialize(typeof (LogWrapperTests));
            return logger;
        }

        private LoggingEvent[] GetAllEvents()
        {
            if (_appender.GetEvents().Length == 0)
                return null;
            return _appender.GetEvents();
        }

        private string GetLogMessage(int i = 0)
        {
            if (_appender.GetEvents().Length == 0)
                return null;

            var logEvent = GetAllEvents()[i];
            return logEvent.MessageObject.ToString();
        }

        private string GetLogException()
        {
            if (_appender.GetEvents().Length == 0)
                return null;
            var logEvent = _appender.GetEvents()[0];
            return logEvent.ExceptionObject.ToString();
        }

        private string GetLogExceptionMessage()
        {
            return $"{_exception.GetType().FullName}: {_exception.Message}";
        }

        private static void LogAllLevelLogs(ILogWrapper logManager)
        {
            logManager.Debug(new IntegrationLogModel() {Message = string.Concat(LogMessage, Level.Debug)});
            logManager.Info(new IntegrationLogModel() {Message = string.Concat(LogMessage, Level.Info)});
            logManager.Warn(new IntegrationLogModel() {Message = string.Concat(LogMessage, Level.Warn)});
            logManager.Error(new IntegrationLogModel() {Message = string.Concat(LogMessage, Level.Error)});
            logManager.Fatal(new IntegrationLogModel() {Message = string.Concat(LogMessage, Level.Fatal)});
        }

        #endregion
    }
}

