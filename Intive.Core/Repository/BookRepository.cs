using Intive.Core.Database;
using Intive.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Intive.Core.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _appDbContext;
        public BookRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public bool Create(Book book)
        {
            _appDbContext.Books.Add(book);
            return true;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _appDbContext.Books.OrderBy(x => x.Title).ToList();
        }

        public Book? GetById(int id)
        {
            return _appDbContext.Books.Find(id);
        }

        public Book? GetByTitle(string title)
        {
            return _appDbContext.Books.FirstOrDefault(x => x.Title == title);
        }

        public bool Update(Book book)
        {
            _appDbContext.Entry(book).State = EntityState.Modified;
            return true;
        }
        public bool Delete(int id)
        {
            var book = _appDbContext.Books.Find(id);
            _appDbContext.Books.Remove(book);
            return true;
        }

        
    }
}
