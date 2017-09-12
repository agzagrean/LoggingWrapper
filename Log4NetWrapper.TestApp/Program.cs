using System;
using System.Web.Script.Serialization;
using Iris.Logging.Logic.Appenders;
using Iris.Logging.Logic.Logger;
using Iris.Logging.Logic.Models;
using Iris.Logging.TestApp.Ioc;
using log4net.Core;
using Microsoft.Practices.Unity;

namespace Iris.Logging.TestApp
{
    class Program
    {
        static ILogWrapper integrationLogger;
        static ILogWrapper auditLogger;
        static ILogWrapper fileLogger;

        static void Main(string[] args)
        {
            Console.WriteLine("-------- Logging Test App ----------");
            try
            {
                //string connectionString = section["connectionString"];
                //string logLevel = section["logLevel"];

                //Console.WriteLine(string.IsNullOrWhiteSpace(connectionString)
                //    ? "Missing connection string"
                //    : $"Connection string for logging db: {connectionString}");

                //Console.WriteLine(string.IsNullOrWhiteSpace(logLevel)
                //    ? "Missing log level"
                //    : $"Connection string for logging db: {connectionString}");
            }
            catch (Exception)
            {

                throw;
            }

            integrationLogger = IocProxy.UnityContainer.Resolve<ILogWrapper>(nameof(IntegrationDbAppender));
            integrationLogger.Initialize(typeof (Program));
            integrationLogger.Info(FormatLogEntry(Level.Info));
            integrationLogger.Debug(FormatLogEntry(Level.Debug));
            integrationLogger.Error(FormatLogEntry(Level.Error));
            integrationLogger.Warn(FormatLogEntry(Level.Warn));
            integrationLogger.Fatal(FormatLogEntry(Level.Fatal));

            auditLogger = IocProxy.UnityContainer.Resolve<ILogWrapper>(nameof(AuditDbAppender));
                //needs to be the name of the calling class
            auditLogger.Initialize(typeof (AuditDbAppender)); //needs to be the name of the calling class
            //     for (int i = 0; i < 2; i++)
            auditLogger.Info(FormatLogEntry(Level.Info));

            fileLogger = IocProxy.UnityContainer.Resolve<ILogWrapper>(nameof(FileAppender));
            fileLogger.Initialize(typeof(FileAppender));

            fileLogger.Debug(FormatLogEntry(Level.Debug));

            Console.ReadLine();
        
}

        private static IntegrationLogModel FormatLogEntry(Level level, int i =0)
        {
            return new IntegrationLogModel()
            {
                PropertyCode = $"Load_{level}_Test",
                MethodName = nameof(Main),
                Message = $"Test Error {level} message - {i}",
                MessageDirection = MessageDirection.Incoming.ToString(),
                ThirdPartySystemId = $"{level} load lest",
                ThirdPartySystemType = $"{level} load lest",
                Request = new JavaScriptSerializer().Serialize("request"),
                Response = new JavaScriptSerializer().Serialize("response"),
                Exception = new Exception("exceptionMessage"),
            };
        }
    }
}
