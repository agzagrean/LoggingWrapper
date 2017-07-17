using System;
using Iris.Logging.Logic.Appenders;
using Iris.Logging.Logic.Logger;
using Microsoft.Azure;
using Microsoft.Practices.Unity;

namespace Iris.Logging.Logic.Unity
{
    public static class LoggingUnityExtensions
    {
        private const string DefaultLogLevel = "INFO";

        public static void RegisterIntegrationLogger(this IUnityContainer container, string logLevel, string name = null)
        {
            if (string.IsNullOrWhiteSpace(logLevel))
                logLevel = DefaultLogLevel;

            var loggingDbConnectionString = CloudConfigurationManager.GetSetting("LoggingDbConnectionString");
            if (string.IsNullOrWhiteSpace(loggingDbConnectionString))
                throw new Exception("The loggingDbConnectionString is not being set");


            container.RegisterType<IntegrationDbAppender>(new ContainerControlledLifetimeManager(), new InjectionFactory(
                    x => new IntegrationDbAppender(loggingDbConnectionString)));

            container.RegisterType<ILogWrapper>(name, new TransientLifetimeManager(), new InjectionFactory(
                    x => new LogWrapper(container.Resolve<IntegrationDbAppender>(), logLevel)));
        }

        public static void RegisterAuditingLogger(this IUnityContainer container, string logLevel, string name = null)
        {
            if (string.IsNullOrWhiteSpace(logLevel))
                logLevel = DefaultLogLevel;

            var loggingDbConnectionString = CloudConfigurationManager.GetSetting("LoggingDbConnectionString");
            if (string.IsNullOrWhiteSpace(loggingDbConnectionString))
                throw new Exception("The loggingDbConnectionString is not being set");

            container.RegisterType<AuditDbAppender>(new ContainerControlledLifetimeManager(), new InjectionFactory(
                    x => new AuditDbAppender(loggingDbConnectionString)));

            container.RegisterType<ILogWrapper>(name, new TransientLifetimeManager(), new InjectionFactory(
                x => new LogWrapper(container.Resolve<AuditDbAppender>(), logLevel)));
        }

        public static void RegisterFileLogger(this IUnityContainer container, string logLevel, string name = null)
        {
            if (string.IsNullOrWhiteSpace(logLevel))
                logLevel = DefaultLogLevel;

            var loggingDbConnectionString = CloudConfigurationManager.GetSetting("LoggingDbConnectionString");
            if (string.IsNullOrWhiteSpace(loggingDbConnectionString))
                throw new Exception("The loggingDbConnectionString is not being set");

            container.RegisterType<FileAppender>(new ContainerControlledLifetimeManager(), new InjectionFactory(
                x => new FileAppender(loggingDbConnectionString)));

            container.RegisterType<ILogWrapper>(name, new TransientLifetimeManager(), new InjectionFactory(
                x => new LogWrapper(container.Resolve<FileAppender>(), logLevel)));
        }

    }
}
