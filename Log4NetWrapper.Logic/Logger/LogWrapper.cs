using System;
using Iris.Logging.Logic.Constants;
using Iris.Logging.Logic.Models;
using log4net;
using log4net.Appender;
using log4net.Repository.Hierarchy;

namespace Iris.Logging.Logic.Logger
{
    public class LogWrapper : ILogWrapper
    {
        private ILog _loggingInstance;
        private readonly IAppender _appender;
        private readonly string _logLevel;

        private const string DefaultLogLevel = "INFO";
        public LogWrapper(IAppender appender, string logLevel)
        {
            _appender = appender;
            _logLevel = string.IsNullOrEmpty(logLevel) ? DefaultLogLevel : logLevel;
        }

        public void Initialize(Type type)
        {
            _loggingInstance = LogManager.GetLogger(type);
            Hierarchy h = (Hierarchy)LogManager.GetRepository();

            log4net.Repository.Hierarchy.Logger currentLogger = (log4net.Repository.Hierarchy.Logger)_loggingInstance.Logger;
            currentLogger.AddAppender(_appender);
            currentLogger.Level = currentLogger.Hierarchy.LevelMap[_logLevel]; 
            var loggers = h.GetCurrentLoggers();

            h.Configured = true;
        }

        public void Info(IntegrationLogModel logEvent)
        {
            if (logEvent == null) return;

            ValidateLoggingInstance();

            Log(_loggingInstance.Info, logEvent);
        }

        public void Debug(IntegrationLogModel logEvent)
        {
            if (logEvent == null) return;

            ValidateLoggingInstance();

            Log(_loggingInstance.Debug, logEvent);
        }

        public void Error(IntegrationLogModel logEvent)
        {
            if (logEvent == null) return;

            ValidateLoggingInstance();

            Log(_loggingInstance.Error, logEvent);
        }

        public void Warn(IntegrationLogModel logEvent)
        {
            if (logEvent == null) return;

            ValidateLoggingInstance();

            Log(_loggingInstance.Warn, logEvent);
        }

        public void Fatal(IntegrationLogModel logEvent)
        {
            if (logEvent == null) return;

            ValidateLoggingInstance();

            Log(_loggingInstance.Fatal, logEvent);
        }

        private void ValidateLoggingInstance()
        {
            if (_loggingInstance == null)
            {

                throw new InvalidOperationException($"Logging instance was null. Check {nameof(ILogWrapper)}.Initialize() was called.");
            }
        }

        #region Private

        private void SetupLog4NetGlobalContext(IntegrationLogModel logEvent)
        {
            LogicalThreadContext.Properties[AppenderConstants.MethodName] = logEvent.MethodName ?? string.Empty;
            LogicalThreadContext.Properties[AppenderConstants.PropertyCode] = logEvent.PropertyCode ?? string.Empty;
            LogicalThreadContext.Properties[AppenderConstants.MessageDirection] = logEvent.MessageDirection ?? string.Empty;
            LogicalThreadContext.Properties[AppenderConstants.ThirdPartySystemId] = logEvent.ThirdPartySystemId ?? string.Empty;
            LogicalThreadContext.Properties[AppenderConstants.ThirdPartySystemType] = logEvent.ThirdPartySystemType ?? string.Empty;
            LogicalThreadContext.Properties[AppenderConstants.Request] = logEvent.Request ?? string.Empty;
            LogicalThreadContext.Properties[AppenderConstants.Response] = logEvent.Response ?? string.Empty;
        }

        private void Log(Action<string,Exception> logAction, IntegrationLogModel logEvent)
        {
            SetupLog4NetGlobalContext(logEvent);
            logAction(logEvent.Message, logEvent.Exception);
        }

        #endregion
    }
}
