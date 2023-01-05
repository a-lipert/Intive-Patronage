using Intive.Business.Models;
using Intive.Core.Entities;

namespace Intive.Business.Services
{
    public interface IBookService
    {
        Book? GetById(int id);
        Book? GetByTitle(string title);
        List<Book> GetAll();
        IEnumerable<Book> SearchBook(string query);
        List<ValidationError> CreateBook(BookModel book);
        List<ValidationError> UpdateBook(int id, BookModel book);
        void DeleteBook(int id);

    }
}
