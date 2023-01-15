using Intive.Business.Models;
using Intive.Core.Entities;

namespace Intive.Business.Services
{
    public interface IBookService
    {
        BookModel GetById(int id);
        BookModel GetByTitle(string title);
        List<BookModel> GetAll();
        IEnumerable<BookModel> SearchBook(string query); 
        List<ValidationError> CreateBook(BookModel book);
        List<ValidationError> UpdateBook(int id, BookModel book);
        void DeleteBook(int id);

    }
}
