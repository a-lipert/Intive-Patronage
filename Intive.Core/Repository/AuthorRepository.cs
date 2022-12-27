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
            return _appDbContext.Authors.FirstOrDefault(x => x.LastName == name);
        }

        public List<Author> GetAll()
        {
            return _appDbContext.Authors.OrderBy(x => x.LastName).ToList();
        }


        public void Create<T>(T entity)
        {
            var author = new Author();
           
            _appDbContext.Authors.Add(author);
            _appDbContext.SaveChanges();
            
        }

       
    }

      
 }

