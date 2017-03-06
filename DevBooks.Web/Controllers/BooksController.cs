using DevBooks.Data;
using DevBooks.Web.ViewModels;
using System;
using System.Collections.Generic;
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
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            _unit.Dispose();
            base.Dispose(disposing);
        }
    }
}