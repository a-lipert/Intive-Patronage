using Intive.Business.Helpers;
using Intive.Business.Models;
using Intive.Core.Enums;
using Intive.Core.Repository;

namespace Intive.Business.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        /// <summary>
        /// Creates book
        /// </summary>
        /// <param name="book">Parameters of a new book</param>
        /// <returns>List of validation errors</returns>
        public List<ValidationError> CreateBook(BookModel book)
        {
            var validationResult = IsValid(book);

            if (!validationResult.Any())
            {
                var bookToCreate = book.ToBookEntity();

                if (_authorRepository.AuthorExists(book.AuthorId))
                {
                    bookToCreate.BookAuthors.Add(new Core.Entities.BookAuthor { AuthorId = book.AuthorId });
                }

                _bookRepository.Create(bookToCreate);
            }

            return validationResult;
        }


        /// <summary>
        /// Updates a book
        /// </summary>
        /// <param name="id">Id of a book to update</param>
        /// <param name="book">Parameters of updated book</param>
        /// <returns>Validation errors</returns>
        public List<ValidationError> UpdateBook(int id, BookModel book)
        {
            var validationResult = IsValid(book);
            if (!_bookRepository.BookExists(id))
            {
                throw new RecordNotFoundException(nameof(book), id);
            }        

            if (!validationResult.Any() && id > 0)
            {
                var bookToUpdate = book.ToBookEntity();
                _bookRepository.Update(id, bookToUpdate);
            }

            return validationResult;
        }
      
        /// <summary>
        /// Gets book by id
        /// </summary>
        /// <param name="id">Book id</param>
        /// <returns>Book</returns>
        public BookModel GetById(int id)
        {
            if (id < 1)
            {
                return null;
            }

            var book = _bookRepository.GetById(id);
            return book.ToBookModel();
        }

        /// <summary>
        /// Gets book by title
        /// </summary>
        /// <param name="title">Book title</param>
        /// <returns>Book model</returns>
        /// <exception cref="ArgumentNullOrEmptyException">Thrown if title is null or empty</exception>
        public BookModel GetByTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullOrEmptyException("title");
            }

            var book = _bookRepository.GetByTitle(title);
            if (book == null)
            {
                return null;
            }

            return book.ToBookModel();
        }

        /// <summary>
        /// Retrieves list of all books
        /// </summary>
        /// <returns>All boks</returns>
        public List<BookModel> GetAll()
        {
            var books = _bookRepository.GetAll();
            return books.Select(x => x.ToBookModel()).ToList();
        }

        /// <summary>
        /// Deletes book by id
        /// </summary>
        /// <param name="id">Id of a book to delete</param>
        public void DeleteBook(int id)
        {
            if (!_bookRepository.BookExists(id))
            {
                throw new ArgumentNullOrEmptyException("id");
            }

             _bookRepository.Delete(id);
        }


        /// <summary>
        ///Search books by search query
        /// </summary>
        /// <param name="query">Search query</param>
        /// <returns>Books containing query</returns>
        /// <exception cref="ArgumentNullOrEmptyException">Thrown if the query is null or empty</exception>
        public IEnumerable<BookModel> SearchBook(string query)
        {
            var books = _bookRepository.SearchBook(query, BookOrderBy.Title, false);

            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentNullOrEmptyException("query");
            }

            if (!books.Any())
            {
                return null;
            }

            return books.Select(x => x.ToBookModel()).ToList();
        }

        private static List<ValidationError> IsValid(BookModel book)
        {
            var validationResults = new List<ValidationError>();

            if (string.IsNullOrEmpty(book.Title))
                validationResults.Add(new ValidationError(ValidationConstants.FieldIsRequired, nameof(book.Title)));
            if (book.Title.Length > 100)
                validationResults.Add(new ValidationError(ValidationConstants.IsTooLong, nameof(book.Title)));
            if (string.IsNullOrEmpty(book.Description))
                validationResults.Add(new ValidationError(ValidationConstants.FieldIsRequired, nameof(book.Description)));
            if (book.Rating.Equals(null))
                validationResults.Add(new ValidationError(ValidationConstants.FieldIsRequired, nameof(book.Rating)));
            if (book.Rating < 0)
                validationResults.Add(new ValidationError("Rating must be greater than 0", nameof(book.Rating)));
            if (book.ISBN.Equals(null))
                validationResults.Add(new ValidationError(ValidationConstants.FieldIsRequired, nameof(book.ISBN)));
            if (book.ISBN.Length != 13)
                validationResults.Add(new ValidationError("ISBN number must be 13 digits long", nameof(book.ISBN)));
            if (book.PublicationDate == DateTime.MinValue)
                validationResults.Add(new ValidationError(ValidationConstants.FieldIsRequired, nameof(book.PublicationDate)));

            return validationResults;

        }
    }
}

