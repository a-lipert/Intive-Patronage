using Intive.Core.Database;
using Intive.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intive.Core.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _appDbContext;
        public AuthorRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public bool Create(Author author)
        {
            _appDbContext.Authors.Add(author);
            return true;
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return _appDbContext.Authors.OrderBy(x => x.LastName).ToList();
        }

        public Author? GetByName(string name)
        {
            return _appDbContext.Authors.FirstOrDefault(x => x.LastName == name);
        }
    }
}
