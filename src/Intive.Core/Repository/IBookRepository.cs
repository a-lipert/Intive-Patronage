using Intive.Core.Entities;
using Intive.Core.Enums;

namespace Intive.Core.Repository
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        Book GetById(int id); 
        Book GetByTitle(string title);
        IEnumerable<Book> SearchBook(string query, BookOrderBy orderBy, bool orderDescending);
        bool Update(int id, Book book);
        void Delete(int id);
        public bool BookExists(int bookId);
    }
}
