using Intive.Core.Entities;

namespace Intive.Core.Repository
{
    public interface IAuthorRepository : IBaseRepository<Author> 
    {
        Author? GetByName(string name);
    }
}
