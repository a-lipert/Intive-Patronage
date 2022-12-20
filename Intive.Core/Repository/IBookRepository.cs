using Intive.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intive.Core.Repository
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAllBooks();
        Book? GetById(int id); 
        Book? GetByTitle(string title);
        bool Create(Book book); 
        bool Update(Book book);
        bool Delete(int id);



    }
}
