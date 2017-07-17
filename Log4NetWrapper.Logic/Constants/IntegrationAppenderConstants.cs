using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iris.Logging.Dal.Entities;
using Iris.Logging.Logic.Models;

namespace Iris.Logging.Logic.Constants
{
    public class AppenderConstants
    {
        public const string ThirdPartySystemId = nameof(IntegrationLogEntity.ThirdPartySystemId);
        public const string ThirdPartySystemType = nameof(IntegrationLogEntity.ThirdPartySystemType);
        public const string PropertyCode = nameof(IntegrationLogEntity.PropertyCode);
        public const string Component = nameof(IntegrationLogEntity.Component);
        public const string Request = nameof(IntegrationLogEntity.Request);
        public const string Response = nameof(IntegrationLogEntity.Response);
        public const string Message = nameof(IntegrationLogEntity.Message);
        public const string MessageDirection = nameof(IntegrationLogEntity.MessageDirection);
        public const string Exception = nameof(IntegrationLogEntity.Exception);
        public const string LogLevel = nameof(IntegrationLogEntity.LogLevel);
        public const string CreatedDateUtc = nameof(IntegrationLogEntity.CreatedDateUtc);
        public static string MethodName = nameof(IntegrationLogModel.MethodName);
    }
}
