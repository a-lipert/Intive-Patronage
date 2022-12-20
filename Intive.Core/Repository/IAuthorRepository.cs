using Intive.Core.Entities;

namespace Intive.Core.Repository
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAllAuthors();
        Author? GetByName(string name);
        bool Create(Author author);
        
    }
}
