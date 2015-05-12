using System;
using System.Configuration;
using System.IO;
using log4net;

namespace beah.Library.Web.Logging
{
    public class Logger
    {
        /// <summary>
        /// Method creates an instance of log4net logging using the settings from the config file
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ILog GetLogger(Type type)
        {
            try
            {
                if (ConfigurationManager.AppSettings["log4net.configfile"] == null)
                {
                    throw new ConfigurationErrorsException("Missing log4net.configfile setting in <appsettings>");
                }
                log4net.Config.XmlConfigurator.Configure(new FileInfo(ConfigurationManager.AppSettings["log4net.configfile"]));
            
            }
            catch (Exception)
            {
                
            }
            ILog logger = LogManager.GetLogger(type);
            return logger;
        }
    }
}
