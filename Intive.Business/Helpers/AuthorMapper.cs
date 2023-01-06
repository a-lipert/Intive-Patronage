using Intive.Business.Models;
using Intive.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intive.Business.Helpers
{
    public static class AuthorMapper
    {
        public static Author ToAuthorEntity(this AuthorModel model)
        {
            return new Author
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthDate = model.BirthDate,
                Gender = model.Gender,
            };

        }

        public static AuthorModel ToAuthorModel(this Author author)
        {
            return new AuthorModel
            {
                FirstName = author.FirstName,
                LastName = author.LastName,
                BirthDate = author.BirthDate,
                Gender = author.Gender,
            };
        }
    }
}
