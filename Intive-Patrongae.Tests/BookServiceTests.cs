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
        private readonly Mock<IAuthorRepository> _authorRepositoryMock = new();
        public BookServiceTests()
        {
            _bookService = new BookService(_bookRepositoryMock.Object, _authorRepositoryMock.Object);
        }

        [Test]
        public void GetById_ReturnsBookWhenIdExists()
        {
            //Arrange

            var bookId = 11;

            var bookTest = new Book
            {
                Id = bookId,
                Title = "BookTest"
            };

            _bookRepositoryMock.Setup(x => x.GetById(bookId)).Returns(bookTest);

            //Act

            var book = _bookService.GetById(bookId);
            var bookFromDb = book.ToBookEntity();

            //Assert

            Assert.AreEqual(bookTest.Title, bookFromDb.Title);
        }

        [Test]
        public void GetByTitle_ReturnsBookWhenExists()
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
        public void GetByTitle_ReturnsExceptionWhenTitleNullOrEmpty()
        {
            {
                Assert.Throws<ArgumentNullOrEmptyException>(() => _bookService.GetByTitle(null));
                Assert.Throws<ArgumentNullOrEmptyException>(() => _bookService.GetByTitle(""));
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

            var bookToDb = book.ToBookEntity();

            _bookRepositoryMock.Setup(x => x.Create(bookToDb)).Equals(validationResultTestBook);

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

            var bookId = 2;

            _bookRepositoryMock.Setup(x => x.Delete(bookId));
            _bookRepositoryMock.Setup(x => x.BookExists(bookId)).Returns(true);

            //Act

            _bookService.DeleteBook(bookId);


            //Assert

            _bookRepositoryMock.Verify(x => x.Delete(bookId));
        }

        [Test]
        public void DeleteBook_ForInvalidId_ThrowsException()
        {
            //Arrange

            var bookId = 10;

            _bookRepositoryMock.Setup(x => x.Delete(bookId));
            _bookRepositoryMock.Setup(x => x.BookExists(bookId)).Returns(false);

            //Assert
            Assert.Throws<ArgumentNullOrEmptyException>(() => _bookService.DeleteBook(bookId));
        }


        [Test]
        public void UpdateBook_ForValidId()
        {
            //Arrange

            var bookId = 2;
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

            _bookRepositoryMock.Setup(x => x.BookExists(bookId)).Returns(true);

            //Act

            _bookService.UpdateBook(bookId, bookToUpdate);


            //Assert

            _bookRepositoryMock.Verify(x => x.Update(It.IsAny<int>(), It.IsAny<Book>()));
        }

        [Test]
        public void UpdateBook_ForInvalidId()
        {
            //Arrange

            var bookId = 2;
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

            _bookRepositoryMock.Setup(x => x.BookExists(bookId)).Returns(false);

            //Act
            Assert.Throws<RecordNotFoundException>(() => _bookService.UpdateBook(bookId, bookToUpdate));


            //Assert
            _bookRepositoryMock.Verify(x => x.Update(It.IsAny<int>(), It.IsAny<Book>()), Times.Never);
        }

        [Test]
        public void SearchBook_ReturnsBooksWhenContainQuery()
        {
            //Arrange

            var searchQuery = "Test";

            var book = new Book
            {
                Title = "BookTest"
            };
            var anotherBook = new Book
            {
                Description = "Test test"
            };

            var booksList = new List<Book> { book, anotherBook };   

            _bookRepositoryMock.Setup(x => x.SearchBook(searchQuery, Core.Enums.BookOrderBy.Title, false)).Returns(booksList);



            //Act

            var searchedBooks = _bookService.SearchBook(searchQuery);
            var searchedBooksToEntity = searchedBooks.Select(x => x.ToBookEntity()).ToList();

            //Assert

            Assert.AreEqual(2, searchedBooksToEntity.Count);    
            CollectionAssert.AreEquivalent(booksList, searchedBooksToEntity);
        }
    }
}

