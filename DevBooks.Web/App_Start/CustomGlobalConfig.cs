using DevBooks.Web.Filters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DevBooks.Web
{
    public class CustomGlobalConfig
    {
        public static void Customize(HttpConfiguration config)
        {
            //ensure that JSON is returned. check bit.ly/10EEHlQ
            config.Formatters.Remove(config.Formatters.XmlFormatter);


            // jpapa approach
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();

            config.Filters.Add(new ValidationActionFilter());
        }
    }
}