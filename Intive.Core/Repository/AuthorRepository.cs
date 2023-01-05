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
       
        public Author? GetByName(string name)
        {
            return _appDbContext.Authors.FirstOrDefault(x => x.LastName == name || x.FirstName == name);
        }

        public List<Author> GetAll()
        {
            return _appDbContext.Authors.OrderBy(x => x.LastName).ToList();
        }


        public void Create(Author entity)
        {            
            _appDbContext.Authors.Add(entity);
            _appDbContext.SaveChanges();            
        }

       
    }

      
 }

