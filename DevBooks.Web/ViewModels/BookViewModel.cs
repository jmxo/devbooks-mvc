using DevBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBooks.Web.ViewModels
{
    public class BookViewModel : ViewModelBase
    {
        public Book Book { get; set; }
        public bool IsNew { get; set; }

        public string ImageUrlPrefix
        {
            get { return DevBooks.Web.Config.ImagesUrlPrefix; }
        }
            
        public BookViewModel()
        {
            this.Book = new Book();
        }
    }
}