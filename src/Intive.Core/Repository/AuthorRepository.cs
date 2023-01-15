using Intive.Core.Database;
using Intive.Core.Entities;

namespace Intive.Core.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _appDbContext;
        public AuthorRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        /// <summary>
        /// Retrieves authors by first or last name
        /// </summary>
        /// <param name="name">First or last name</param>
        /// <returns>Author entity</returns>
        public Author GetByName(string name)
        {
            return _appDbContext.Authors.FirstOrDefault(x => x.LastName == name || x.FirstName == name);
        }

       /// <summary>
       /// Retrieves all authors
       /// </summary>
       /// <returns>List of Authors</returns>
        public List<Author> GetAll()
        {
            return _appDbContext.Authors.OrderBy(x => x.LastName).ToList();
        }

        /// <summary>
        /// Creates author entity and adds to db
        /// </summary>
        /// <param name="entity">Parameters of a new author entity</param>
        public void Create(Author entity)
        {
            _appDbContext.Authors.Add(entity);
            _appDbContext.SaveChanges();
        }

        /// <summary>
        /// Checks if there is any author corresponding to the id in the db
        /// </summary>
        /// <param name="id">Author id</param>
        /// <returns>True if author with the id exists</returns>
        public bool AuthorExists(int id)
        {
            return _appDbContext.Authors.Any(x => x.Id == id);
        }

    }


}

