using Intive.Core.Entities;

namespace Intive.Core.Repository
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        Book GetById(int id); 
        Book GetByTitle(string title);
        Book SearchBookByTitlePart(string query);
        bool Update(int id, Book book);
        void Delete(int id);
    }
}
