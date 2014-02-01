using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;

namespace MTNet.Core
{
    public class Configuration : ConfigurationSection
    {
        static Configuration()
        {
            Instance = (Configuration) ConfigurationManager.GetSection("mtNet");
        }

        public static Configuration Instance { get; private set; }

        [ConfigurationProperty("dataPath", IsRequired = true)]
        public string DataPath
        {
            get
            {
                // Clean up the path and handle and relative and parent pathing issues
                var configValue = this["dataPath"].ToString();
                configValue = configValue.Replace("~", AppDomain.CurrentDomain.BaseDirectory);
                return new DirectoryInfo(configValue).FullName;
            }
        }

        [ConfigurationProperty("defaultFileName", DefaultValue = "index", IsRequired = false)]
        public string DefaultFileName
        {
            get { return (string) this["defaultFileName"]; }
        }

        [ConfigurationProperty("manifestFileName", DefaultValue = "manifest.xml", IsRequired = false)]
        public string ManifestFileName
        {
            get { return (string) this["manifestFileName"]; }
        }

        [ConfigurationProperty("pathPrefix", DefaultValue = "/", IsRequired = false)]
        public string PathPrefix
        {
            get { return (string) this["pathPrefix"]; }
        }

        [ConfigurationProperty("contentControllerName", DefaultValue = "content", IsRequired = false)]
        public string ContentControllerName
        {
            get { return (string) this["contentControllerName"]; }
        }

        [ConfigurationProperty("controllerActionName", DefaultValue = "display", IsRequired = false)]
        public string ControllerActionName
        {
            get { return (string) this["controllerActionName"]; }
        }

        [ConfigurationProperty("disallowedPaths", DefaultValue = "", IsRequired = false)]
        [TypeConverter(typeof (CommaDelimitedStringCollectionConverter))]
        public CommaDelimitedStringCollection DisallowedPaths
        {
            get { return (CommaDelimitedStringCollection) base["disallowedPaths"]; }
        }
    }
}