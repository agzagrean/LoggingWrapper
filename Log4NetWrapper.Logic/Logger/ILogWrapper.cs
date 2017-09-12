using System;
using Iris.Logging.Logic.Models;

namespace Iris.Logging.Logic.Logger
{
    public interface ILogWrapper
    {
        void Initialize(Type type);

        void Info(IntegrationLogModel logEvent);
        void Debug(IntegrationLogModel logEvent);
        void Error(IntegrationLogModel logEvent);
        void Warn(IntegrationLogModel logEvent);
        void Fatal(IntegrationLogModel logEvent);

    }
}
