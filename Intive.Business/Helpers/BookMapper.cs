using Intive.Business.Models;
using Intive.Core.Entities;

namespace Intive.Business.Helpers
{
    public static class BookMapper
    {
        public static Book ToBookEntity(this BookModel book)
        {
            if (book is null) return null;

            return new Book
            {
                Title = book.Title,
                Description = book.Description,
                Rating = book.Rating,
                ISBN = book.ISBN,
                PublicationDate = book.PublicationDate,
                BookAuthors = new List<BookAuthor>()
            };

        }

        public static BookModel ToBookModel(this Book book)
        {
            if (book is null) return null;

            return new BookModel
            {
                Title = book.Title,
                Description = book.Description,
                Rating = book.Rating,
                ISBN = book.ISBN,
                PublicationDate = book.PublicationDate,
                AuthorId = book.BookAuthors?.FirstOrDefault()?.AuthorId ?? -1,
                AuthorName = book.BookAuthors?.FirstOrDefault()?.Author?.FullName
            };
        }
    }
}
