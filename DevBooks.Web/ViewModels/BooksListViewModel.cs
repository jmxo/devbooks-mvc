using DevBooks.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBooks.Web.ViewModels
{
    public class BooksListViewModel : ViewModelBase
    {
        public IList<Book> Books { get; set; }

        public string BooksJSON
        {
            get
            {
                JsonSerializerSettings settings =
                    new JsonSerializerSettings();

                settings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver();

                var books = JsonConvert.SerializeObject(this.Books, settings);
                return books;
            }
        }

        public string ImageUrlPrefix
        {
            get { return DevBooks.Web.Config.ImagesUrlPrefix; }
        }
    }
}