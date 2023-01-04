using Intive.Core.Database;
using Intive.Core.Entities;
using System.Reflection.Metadata.Ecma335;

namespace Intive.Core.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _appDbContext;
        public BookRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Book GetById(int id)
        {
            return _appDbContext.Books.Find(id);
        }

        public Book GetByTitle(string title)
        {
            return _appDbContext.Books.FirstOrDefault(x => x.Title == title);
        }

        public Book SearchBookByTitlePart(string query)
        {
            var book = _appDbContext.Books.FirstOrDefault(x => x.Title.Contains(query));
            return book;
        }

        public bool Update(int id, Book book)
        {
            var bookToUpdate = _appDbContext.Books.Find(id);
            if (bookToUpdate != null)
            {
                bookToUpdate.Title = book.Title;
                bookToUpdate.Description = book.Description;
                bookToUpdate.Rating = book.Rating;
                bookToUpdate.ISBN = book.ISBN;
                bookToUpdate.PublicationDate = book.PublicationDate;

                _appDbContext.SaveChanges();
                return true;

            };
            return false;
        }

        public void Delete(int id)
        {
            var bookToDelete = _appDbContext.Books.Find(id);
            if (bookToDelete != null) 
            _appDbContext.Books.Remove(bookToDelete);
            _appDbContext.SaveChanges();
        }

        public void Create<T>(T entity)
        {
            var book = new Book();

            _appDbContext.Books.Add(book);
            _appDbContext.SaveChanges();
        }

        public List<Book> GetAll()
        {
            return _appDbContext.Books.OrderBy(x => x.Title).ToList();
        }
    }
}
