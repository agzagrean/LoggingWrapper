using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;

namespace Iris.Logging.Logic.Configuration
{
    public class LoggingConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("Parameters", IsRequired = true, IsDefaultCollection = true)]
        public LoggingConfigCollection Parameters
        {
            get { return (LoggingConfigCollection) base["Parameters"]; }
        }


        public string GetDbConnectionString()
        {
            return GetConfigValue("DbConnectionString");
        }

        private string GetConfigValue(string name)
        {
            foreach (LoggingConfigElement configElement in Parameters)
            {
                if (configElement.Name.Equals(name))
                    return configElement.Value;

            }
            throw new ConfigurationErrorsException($"Missing {name} from config file.");
        }

        public string GetLogLevel()
        {
            return GetConfigValue("LogLevel");
        }
    }

    public class LoggingConfigCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new LoggingConfigElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((LoggingConfigElement) element).Name;
        }
    }

    public class LoggingConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string) base["name"]; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("value", IsRequired = true)]
        public string Value
        {
            get { return (string) base["value"]; }
            set { base["value"] = value; }
        }
    }

}
