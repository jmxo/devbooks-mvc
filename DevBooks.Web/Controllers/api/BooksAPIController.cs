using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DevBooks.Data;
using DevBooks.Models;
using System.Data.Entity.Infrastructure;

namespace DevBooks.Web.Controllers
{
    [Authorize]
    public class BooksAPIController : ApiController
    {
        private ApplicationUnit _unit = new ApplicationUnit();

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Book> Get()
        {
            return _unit.Books.GetAll();
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Manager, User")]
        public Book Get(int id)
        {
            Book book = _unit.Books.GetById(id);
            if (book == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return book;
        }

        [Authorize(Roles = "Admin, Manager, User")]
        public HttpResponseMessage Put(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != book.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            Book existingBook = _unit.Books.GetById(id);
            _unit.Books.Detach(existingBook);

            // Keep original CreatedOn value (to make sure it was not changed)
            book.CreatedOn = existingBook.CreatedOn;

            _unit.Books.Update(book);

            try
            {
                _unit.SaveChanges();

                // Return an explicit value to avoid the fail callback
                // being incorrectly invoked.
                return Request.
                    CreateResponse(HttpStatusCode.OK,
                    "{success:'true', verb:'PUT'}");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.
                    CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
            catch (Exception ex)
            {
                return Request.
                    CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager, User")]
        public HttpResponseMessage Post(Book book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unit.Books.Add(book);
                    _unit.SaveChanges();

                    HttpResponseMessage result =
                        Request.CreateResponse(HttpStatusCode.Created, book);

                    result.Headers.Location =
                        new Uri(Url.Link("DefaultApi", new { id = book.Id }));

                    return result;
                }
                else
                {
                    return Request.
                        CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (Exception ex)
            {
                return Request.
                    CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Admin, Manager")]
        public HttpResponseMessage Delete(int id)
        {
            Book book = _unit.Books.GetById(id);

            if (book == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            _unit.Books.Delete(book);

            try
            {
                _unit.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, book);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.
                    CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
            catch (Exception ex)
            {
                return Request.
                    CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
