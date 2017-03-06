using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DevBooks.Web
{
    public class Config
    {
        public static string ImagesFolderPath
        {
            get
            {
                if (ConfigurationManager.AppSettings["ImagesFolderPath"]
                    != null)
                {
                    return ConfigurationManager
                        .AppSettings["ImagesFolderPath"].ToString();
                }

                return "~/img/homes";
            }
        }

        public static string ImagesUrlPrefix
        {
            get { return Config.ImagesFolderPath.Replace("~", ""); }
        }
    }
}