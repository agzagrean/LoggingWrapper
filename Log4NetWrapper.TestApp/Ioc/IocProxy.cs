using System;
using System.Configuration;
using Iris.Logging.Logic.Unity;
using Microsoft.Practices.Unity;
using Iris.Logging.Logic.Appenders;

namespace Iris.Logging.TestApp.Ioc
{
    public static class IocProxy
    {
        private static IUnityContainer _unityContainer;

        public static IUnityContainer UnityContainer
        {
            get
            {
                if (_unityContainer == null)
                {
                    _unityContainer = new UnityContainer();

                    string logLevel = ConfigurationManager.AppSettings?["LogLevel"];
                    _unityContainer.RegisterIntegrationLogger(logLevel, nameof(IntegrationDbAppender));
                    _unityContainer.RegisterAuditingLogger(logLevel, nameof(AuditDbAppender));
                    _unityContainer.RegisterFileLogger(logLevel, nameof(FileAppender));
                }
                return _unityContainer;
            }

            set { _unityContainer = value; }
        }

    
    }
}
