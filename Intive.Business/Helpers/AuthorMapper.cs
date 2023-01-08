using Intive.Business.Models;
using Intive.Core.Entities;

namespace Intive.Business.Helpers
{
    public static class AuthorMapper
    {
        public static Author ToAuthorEntity(this AuthorModel author)
        {
            if(author is null) return null; 
            return new Author
            {
                FirstName = author.FirstName,
                LastName = author.LastName,
                BirthDate = author.BirthDate,
                Gender = author.Gender,
            };

        }

        public static AuthorModel ToAuthorModel(this Author author)
        {
            if(author is null) return null;

            return new AuthorModel
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                BirthDate = author.BirthDate,
                Gender = author.Gender,
            };
        }
    }
}
