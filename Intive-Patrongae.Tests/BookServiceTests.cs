using Intive.Business.Helpers;
using Intive.Business.Models;
using Intive.Business.Services;
using Intive.Core.Entities;
using Intive.Core.Repository;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Intive.Tests
{
    public class BookServiceTests
    {
        private readonly BookService _bookService;
        private readonly Mock<IBookRepository> _bookRepositoryMock = new();
        public BookServiceTests()
        {
            _bookService = new BookService(_bookRepositoryMock.Object);
        }

        [Test]
        public void GetById_ReturnsBookWhenIdExists()
        {
            //Arrange

            var bookId = 11;

            var bookTest = new Book
            {
                Id = bookId
            };


            _bookRepositoryMock.Setup(x => x.GetById(bookId)).Returns(bookTest);

            //Act

            var book = _bookService.GetById(bookId);


            //Assert

            Assert.AreEqual(bookId, book.Id);
        }

        [Test]
        public void GetByTitle_ReturnsBookWhenNameExists()
        {
            //Arrange

            var bookTitle = "The Book";

            var bookTest = new Book
            {
                Title = bookTitle
            };


            _bookRepositoryMock.Setup(x => x.GetByTitle(bookTitle)).Returns(bookTest);

            //Act

            var book = _bookService.GetByTitle(bookTitle);


            //Assert

            Assert.AreEqual(bookTitle, book.Title);
        }

        [Test]
        public void GetByName_ReturnsExceptionWhenTitleNull()
        {
            {
                Assert.Throws<ArgumentNullException>(() => _bookService.GetByTitle(null));
            }
        }

        [Test]
        public void GetAll_ReturnsAllBooks()
        {

            //Arrange

            var allBooks = new List<Book>()
                {
                   new Book {Id = 1, Title = "Hobbit", Description = "Hobbit description", Rating = 9.77m, ISBN = "1234567891111" , PublicationDate = new DateTime(1937,9,21) },
                   new Book {Id = 2, Title = "Harry Potter", Description = "Harry Potter description", Rating = 9.22m, ISBN = "1111987654321" , PublicationDate = new DateTime(1997,6,26),}
                };

            _bookRepositoryMock.Setup(x => x.GetAll()).Returns(allBooks);

            //Act

            var books = _bookService.GetAll();

            //Assert

            Assert.AreEqual(allBooks.Count, books.Count);

        }

        [Test]
        public void CreateBook_ReturnsValidationErrors()
        {

            //Arrange

            var book = new BookModel()
            {
                Title = "",
                Description = "",
                Rating = -8.2m,
                ISBN = "1234",
            };

            _bookRepositoryMock.Setup(x => x.Create(book)).Equals(validationResultTestBook);

            //Act

            var bookToCreate = _bookService.CreateBook(book);

            //Assert

            Assert.IsAssignableFrom<List<ValidationError>>(bookToCreate);
            Assert.AreEqual(validationResultTestBook, bookToCreate);

        }

        private readonly List<ValidationError> validationResultTestBook = new()
            {
                new ValidationError(ValidationConstants.FieldIsRequired, "Title"),
                new ValidationError(ValidationConstants.FieldIsRequired, "Description"),
                new ValidationError("Rating must be greater than 0", "Rating"),
                new ValidationError("ISBN number must be 13 digits long", "ISBN"),
                new ValidationError(ValidationConstants.FieldIsRequired, "PublicationDate"),

            };


        [Test]
        public void DeleteBook_ForValidId()
        {
            //Arrange

            var bookId = 1;

            _bookRepositoryMock.Setup(x => x.Delete(bookId));

            //Act

            _bookService.DeleteBook(bookId);


            //Assert

            _bookRepositoryMock.Verify(x => x.Delete(bookId));
        }

        [Test]
        public void UpdateBook_ForValidId()
        {
            //Arrange

            var bookId = 1;
            var bookToUpdate = new BookModel()
            {
                Title = "Title",
                Description = "Description",
                Rating = 4.5m,
                ISBN = "1234567891234",
                PublicationDate = new DateTime(2022, 1, 1)
            };


            _bookRepositoryMock.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Book>()))
                   .Returns(true);

            //Act

            _bookService.UpdateBook(bookId, bookToUpdate);


            //Assert

            _bookRepositoryMock.Verify(x => x.Update(It.IsAny<int>(), It.IsAny<Book>()));
        }

        [Test]
        public void SearchBookByTitlePart_ReturnsBookWhenExists()
        {
            //Arrange

            var searchQuery = "Harry";

            var book = new Book
            {
                Title = "Harry Potter"
            };

            _bookRepositoryMock.Setup(x => x.SearchBookByTitlePart(searchQuery)).Returns(book);

            //Act

            var searchedBook = _bookService.SearchBookByTitlePart(searchQuery);

            //Assert

            Assert.AreEqual(book.Title, searchedBook.Title);

        }

    }




}

