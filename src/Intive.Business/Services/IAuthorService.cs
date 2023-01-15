using Intive.Business.Models;

namespace Intive.Business.Services
{
    public interface IAuthorService
    {
         List<ValidationError> CreateAuthor(AuthorModel author);
         AuthorModel GetByName(string name);
         List<AuthorModel> GetAll();
    }
}
