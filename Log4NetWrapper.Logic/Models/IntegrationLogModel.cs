using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iris.Logging.Dal.Entities;
using log4net.Core;

namespace Iris.Logging.Logic.Models
{
    public class IntegrationLogModel
    {
        public string PropertyCode { get; set; }
        public string MessageDirection { get; set; }
        public string ThirdPartySystemId { get; set; }
        public string ThirdPartySystemType { get; set; }
        public string MethodName { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public string LogLevel { get; set; }
    }
}
