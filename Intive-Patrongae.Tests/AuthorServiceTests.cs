using Intive.Business.Services;
using Intive.Core.Entities;
using Intive.Core.Repository;
using Moq;
using NUnit.Framework;
using Intive.Core.Entities.Enums;
using Intive.Business.Models;
using Intive.Business.Helpers;


namespace Intive.Tests
{
    public class AuthorServiceTests
    {
        private readonly AuthorService _authorService;
        private readonly Mock<IAuthorRepository> _authorRepositoryMock = new();

        public AuthorServiceTests()
        {
            _authorService = new AuthorService(_authorRepositoryMock.Object);
        }

        [Test]
        public void GetByName_ReturnsAuthorWhenNameExists()
        {
            //Arrange

            var authorName = "Tolkien";

            var authorTest = new Author
            {
                LastName = authorName
            };


            _authorRepositoryMock.Setup(x => x.GetByName(authorName)).Returns(authorTest);

            //Act

            var author = _authorService.GetByName(authorName);


            //Assert

            Assert.AreEqual(authorName, author.LastName);

        }

        [Test]
        public void GetByName_ReturnsExceptionWhenNameNullOrEmpty()
        {
            {
                Assert.Throws<ArgumentException>(() => _authorService.GetByName(null));
                Assert.Throws<ArgumentException>(() => _authorService.GetByName(""));
            }

        }

        [Test]
        public void GetAll_ReturnsAllAuthors()
        {
            {
                //Arrange

                var allAuthors = new List<Author>()
                {
                   new Author {Id = 1, FirstName = "J.R.R.", LastName = "Tolkien", BirthDate = new DateTime(1892,1,3), Gender = Gender.Male },
                   new Author {Id = 2, FirstName = "J.K", LastName = "Rowling", BirthDate = new DateTime(1965,7,31), Gender = Gender.Female }
                };

                _authorRepositoryMock.Setup(x => x.GetAll()).Returns(allAuthors);

                //Act

                var authors = _authorService.GetAll();

                //Assert

                Assert.AreEqual(allAuthors.Count, authors.Count);
            }
        }

        [Test]
        public void CreateAuthor_ReturnsValidationErrors()
        {
            {
                //Arrange

                var author = new AuthorModel()
                {
                    FirstName = "CharlesCharlesCharlesCharlesCharlesCharlesCharlesCharles",
                    LastName = "DickensDickensDickensDickensDickensDickensDickensDickens",
                };
                var authorToDb = author.ToAuthorEntity();
                _authorRepositoryMock.Setup(x => x.Create(authorToDb)).Equals(validationResultTest);

                //Act

                var authorToCreate = _authorService.CreateAuthor(author);

                //Assert

                Assert.IsAssignableFrom<List<ValidationError>>(authorToCreate);
                Assert.AreEqual(validationResultTest, authorToCreate);  

            }
        }

        private readonly List<ValidationError> validationResultTest = new()
            {
                new ValidationError(ValidationConstants.IsTooLong, "FirstName"),
                new ValidationError(ValidationConstants.IsTooLong, "LastName"),
                new ValidationError(ValidationConstants.FieldIsRequired, "BirthDate"),
            };


    }





}
