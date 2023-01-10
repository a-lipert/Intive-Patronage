using Intive.Business.Helpers;
using Intive.Business.Models;
using Intive.Core.Repository;
using static Intive.Business.Services.AuthorService;

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

        public List<ValidationError> CreateBook(BookModel book)
        {            
            var validationResult = IsValid(book);
            if (!validationResult.Any())
            {
                var bookToCreate = book.ToBookEntity();

                if (_authorRepository.Exists(book.AuthorId))
                {
                    bookToCreate.BookAuthors.Add(new Core.Entities.BookAuthor { AuthorId = book.AuthorId });
                }

                _bookRepository.Create(bookToCreate);
            }

            return validationResult;
        }


        public List<ValidationError> UpdateBook(int id, BookModel book)
        {
            var validationResult = IsValid(book);
            if (!validationResult.Any() && id > 0)
            {
                var bookToUpdate = book.ToBookEntity();
                _bookRepository.Update(id, bookToUpdate);
            }

            return validationResult;
        }

        private List<ValidationError> IsValid(BookModel book)
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

        public BookModel GetById(int id)
        {
            if (id < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than 0");
            }

            var book = _bookRepository.GetById(id);
            return book.ToBookModel();
        }
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

        public List<BookModel> GetAll()
        {
            var books = _bookRepository.GetAll();
            return books.Select(x => x.ToBookModel()).ToList();
        }

        public void DeleteBook(int id)
        {
            if (id < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than 0");
            }

            _bookRepository.Delete(id);
        }

        public IEnumerable<BookModel> SearchBook(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentNullOrEmptyException("query");
            }

            var books = _bookRepository.SearchBook(query);
            return books.Select(x => x.ToBookModel()).ToList();
        }
    }
} 

