using DevBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBooks.Data
{
    public class ApplicationUnit : IDisposable
    {
        private DevBooksDbContext _context = new DevBooksDbContext();

        private IRepository<Book> _books = null;

        public IRepository<Book> Books
        {
            get
            {
                if (this._books == null)
                {
                    this._books = new BookRepository(this._context);
                }
                return this._books;
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }
}
