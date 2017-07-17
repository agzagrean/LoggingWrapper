using System;
using System.Data;
using Iris.Logging.Logic.Constants;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace Iris.Logging.Logic.Appenders
{
    public class FileAppender : RollingFileAppender, ILoggingAppender
    {
        public FileAppender(string connectionString)
        {

            ConfigureAppender(connectionString);
        }

        public void ConfigureAppender(string conenctionString)
        {
            Name = nameof(FileAppender);
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

            PatternLayout patternLayout = new PatternLayout();

            string conversationPattern = 
                $"%level - %property{AppenderConstants.MessageDirection} - %property{AppenderConstants.MessageDirection}" +
                $" - %property{AppenderConstants.ThirdPartySystemId} - %property{AppenderConstants.ThirdPartySystemType}" +
                " - %utcdate{yyyy - MM - ddTHH:mm:ss.fff}" +
                $" - property code: {AppenderConstants.PropertyCode} - %logger{2} - %property{AppenderConstants.MethodName}" +
                $" - Request: %property{AppenderConstants.Request} - Response: %property{AppenderConstants.Response}" +
                $" - Message: %message - Exception: %exception %newline";
            patternLayout.ConversionPattern = conversationPattern;
            patternLayout.ActivateOptions();
            RollingFileAppender roller = new RollingFileAppender();
            roller.AppendToFile = true;
            roller.File = @"C:\Logs\log - "+ DateTime.UtcNow.Date.ToLongDateString() + ".txt";
            roller.Layout = patternLayout;
            roller.MaxSizeRollBackups = 5;
            roller.MaximumFileSize = "1GB";
            roller.RollingStyle = RollingFileAppender.RollingMode.Size;
            roller.StaticLogFileName = true;
            roller.ActivateOptions();
            hierarchy.Root.AddAppender(roller);

            MemoryAppender memory = new MemoryAppender();
            memory.ActivateOptions();
            hierarchy.Root.AddAppender(memory);
          //  base.ActivateOptions();
        }
    }
}
