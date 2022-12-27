using Intive.Business.Models;
using Intive.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intive.Business.Services
{
    public interface IAuthorService
    {
         List<ValidationError> CreateAuthor(AuthorModel author);
         Author? GetByName(string name);
         List<Author> GetAll();
    }
}
