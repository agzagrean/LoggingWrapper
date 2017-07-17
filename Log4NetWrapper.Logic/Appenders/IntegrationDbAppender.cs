using System.Data;
using Iris.Logging.Logic.Constants;
using log4net.Appender;
using log4net.Layout;

namespace Iris.Logging.Logic.Appenders
{
    public class IntegrationDbAppender : AdoNetAppender, ILoggingAppender
    {
        public IntegrationDbAppender(string connectionString)
        {
            ConfigureAppender(connectionString);
        }

        public void ConfigureAppender(string connectionString)
        {
            Name = nameof(IntegrationDbAppender);
            BufferSize = 1;
            ConnectionString = connectionString;
            CommandText = $"INSERT INTO IntegrationLogging (" +
                          $"[{AppenderConstants.LogLevel}] ," +
                          $"[{AppenderConstants.MessageDirection}]," +
                          $"[{AppenderConstants.ThirdPartySystemId}] ," +
                          $"[{AppenderConstants.ThirdPartySystemType}] ," +
                          $"[{AppenderConstants.PropertyCode}] ," +
                          $"[{AppenderConstants.CreatedDateUtc}] ," +
                          $"[{AppenderConstants.Component}] ," +
                          $"[{AppenderConstants.Request}] ," +
                          $"[{AppenderConstants.Response}] ," +
                          $"[{AppenderConstants.Message}] ," +
                          $"[{AppenderConstants.Exception}]) " +
                          $"VALUES (" +
                          $"@{AppenderConstants.LogLevel}, @{AppenderConstants.MessageDirection}, " +
                          $"@{AppenderConstants.ThirdPartySystemId}, @{AppenderConstants.ThirdPartySystemType}, " +
                          $"@{AppenderConstants.PropertyCode}, @{AppenderConstants.CreatedDateUtc}, " +
                          $"@{AppenderConstants.Component}, @{AppenderConstants.Request}, @{AppenderConstants.Response}, " +
                          $"@{AppenderConstants.Message}, @{AppenderConstants.Exception})";

            ConnectionType =
                "System.Data.SqlClient.SqlConnection, System.Data, Version=1.0.3300.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

            AddParameter($"@{AppenderConstants.LogLevel}", DbType.String, 10, "%level");
            AddParameter($"@{AppenderConstants.MessageDirection}", DbType.String, 10,
                "%property{" + AppenderConstants.MessageDirection + "}");
            AddParameter($"@{AppenderConstants.ThirdPartySystemId}", DbType.String, 20,
                "%property{" + AppenderConstants.ThirdPartySystemId + "}");
            AddParameter($"@{AppenderConstants.ThirdPartySystemType}", DbType.String, 20,
                "%property{" + AppenderConstants.ThirdPartySystemType + "}");
            AddParameter($"@{AppenderConstants.PropertyCode}", DbType.String, 50,
                "%property{" + AppenderConstants.PropertyCode + "}");
            AddParameter(new AdoNetAppenderParameter()
            {
                ParameterName = $"@{AppenderConstants.CreatedDateUtc}",
                DbType = DbType.DateTime,
                Layout = new Layout2RawLayoutAdapter(new PatternLayout("%utcdate{yyyy-MM-ddTHH:mm:ss.fff}"))
            });
            AddParameter($"@{AppenderConstants.Component}", DbType.String, 255,
                "%logger{2} - %property{" + AppenderConstants.MethodName + "}");
            AddParameter($"@{AppenderConstants.Request}", DbType.String, 4000,
                "%property{" + AppenderConstants.Request + "}");
            AddParameter($"@{AppenderConstants.Response}", DbType.String, 4000,
                "%property{" + AppenderConstants.Response + "}");
            AddParameter($"@{AppenderConstants.Message}", DbType.String, 255, "%message");
            AddParameter($"@{AppenderConstants.Exception}", DbType.String, 4000, "%exception");

            base.ActivateOptions();
        }
        private void AddParameter(string name, DbType dbType, int size, string conversionPattern)
        {
            AddParameter(new AdoNetAppenderParameter()
            {
                ParameterName = name,
                DbType = dbType,
                Size = size,
                Layout = new Layout2RawLayoutAdapter(new PatternLayout(conversionPattern))
            });
        }
    }
}
