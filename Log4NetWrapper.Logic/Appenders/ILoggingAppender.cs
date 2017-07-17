using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Appender;

namespace Iris.Logging.Logic.Appenders
{
    public interface ILoggingAppender: IAppender
    {
        void ConfigureAppender(string connectionString);
    }
}
