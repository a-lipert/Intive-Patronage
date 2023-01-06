using Intive.Business.Models;
using Intive.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intive.Business.Helpers
{
    public static class BookMapper
    {
        public static Book ToBookEntity(this BookModel model)
        {
            return new Book
            {
                Title = model.Title,
                Description = model.Description,
                Rating = model.Rating,
                ISBN = model.ISBN,
                PublicationDate = model.PublicationDate,
                BookAuthors = new List<BookAuthor>()
            };

        }

        public static BookModel ToBookModel(this Book book)
        {
            return new BookModel
            {
                Title = book.Title,
                Description = book.Description,
                Rating = book.Rating,
                ISBN = book.ISBN,
                PublicationDate = book.PublicationDate,
            };
        }
    }
}
