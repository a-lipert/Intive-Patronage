using Intive.Business.Models;
using Intive.Core.Database;
using Intive.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intive.Business.Services
{
    public interface IBookService
    {
        Book? GetById(int id);
        Book? GetByTitle(string title);
        List<Book> GetAll();
        Book SearchBookByTitlePart(string query);
        List<ValidationError> CreateBook(BookModel book);
        List<ValidationError> UpdateBook(int id, BookModel book);
        void DeleteBook(int id);
       



       


    }
}
