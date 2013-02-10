using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using WebGrease.Css.Extensions;

namespace OnlineFileSystem.Models
{
    public class AppConfigConfiguration<TSettings> : IConfiguration<TSettings> where TSettings : new()
    {
        public TSettings Settings { get; private set; }

        public AppConfigConfiguration()
            : this(ConfigurationManager.AppSettings)
        {
        }

        //You can easily unit test this class by giving it a NameValueCollection.
        internal AppConfigConfiguration(NameValueCollection appSettings)
        {
            Settings = new TSettings();

            //Get properties we can potentially write from
            var properties = from prop in typeof(TSettings).GetProperties()
                             where prop.CanWrite
                             where prop.CanRead
                             let setting = appSettings[typeof(TSettings).Name + "." + prop.Name]
                             where setting != null
                             where TypeDescriptor.GetConverter(prop.PropertyType).CanConvertFrom(typeof(string))
                             let value = TypeDescriptor.GetConverter(prop.PropertyType).ConvertFromString(setting)
                             select new { prop, value };

            
            //Assign the properties.
            properties.ForEach(p => p.prop.SetValue(Settings, p.value, null));
        }
    }
}