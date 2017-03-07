using DevBooks.Data;
using DevBooks.Models;
using DevBooks.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevBooks.Web.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private ApplicationUnit _unit = new ApplicationUnit();

        [AllowAnonymous]
        public ActionResult Index()
        {
            BooksListViewModel vm = new BooksListViewModel();
            var query = _unit.Books.GetAll().OrderBy(b => b.Title);
            vm.Books = query.ToList();
            return View(vm);
        }

        public ActionResult New()
        {
            BookViewModel vm = new BookViewModel();
            vm.IsNew = true;

            return View("Book", vm);
        }

        [ActionName("Edit")]
        public ActionResult Get(int id)
        {
            BookViewModel vm = new BookViewModel();

            vm.Book = this._unit.Books.GetById(id);

            if (vm.Book != null)
            {
                return View("Book", vm);
            }

            return View("NotFound");
        }

        [HttpPost()]
        public ActionResult UploadImage(HttpPostedFileBase image, int id)
        {
            JsonResult result;
            Book book;
            Random rand = new Random();
            string unique;
            string ext;
            string fileName;
            string path;

            unique = rand.Next(1000000).ToString();

            ext = Path.GetExtension(image.FileName).ToLower();

            fileName = string.Format("{0}-{1}{2}", id, unique, ext);

            path = Path.Combine(HttpContext.Server.MapPath(Config.ImagesFolderPath), fileName);

            if (ext == ".png" || ext == ".jpg")
            {
                book = _unit.Books.GetById(id);

                if (book != null)
                {
                    book.ImageName = fileName;
                    _unit.Books.Update(book);
                    _unit.SaveChanges();

                    image.SaveAs(path);
                    result = this.Json(new
                    {
                        imageUrl = string.Format("{0}{1}",
                        Config.ImagesUrlPrefix, fileName)
                    });
                }
                else
                {
                    result = this.Json(new
                    {
                        status = "error",
                        statusText =
                            string.Format("There is no book with the Id of " +
                                "'{0}' in the system.", id)
                    });
                }
            }
            else
            {
                result = this.Json(new
                {
                    status = "error",
                    statusText = "Unsupported image type. Only .png or " +
                        ".jpg files are acceptable."
                });
            }

            return result;
        }

        protected override void Dispose(bool disposing)
        {
            _unit.Dispose();
            base.Dispose(disposing);
        }
    }
}