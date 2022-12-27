using Intive.Business.Helpers;
using Intive.Business.Models;
using Intive.Core.Entities;
using Intive.Core.Repository;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intive.Business.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public List<ValidationError> CreateBook(BookModel book)
        {
            var validationResult = IsValid(book);
            if (validationResult.Any()) return validationResult;
            else
            {
                var bookToCreate = new Book()
                {
                    Title = book.Title,
                    Description = book.Description,
                    Rating = book.Rating,
                    ISBN = book.ISBN,
                    PublicationDate = book.PublicationDate,
                };

                _bookRepository.Create(bookToCreate);

            }
            return validationResult;
        }


        public List<ValidationError> UpdateBook(int id, BookModel book)
        {
            var validationResult = IsValid(book);
            if (validationResult.Any()) return validationResult;
            else if (id>0)
            {
                var bookToUpdate = new Book()
                {
                    Title = book.Title,
                    Description = book.Description,
                    Rating = book.Rating,
                    ISBN = book.ISBN,
                    PublicationDate = book.PublicationDate,
                };

                _bookRepository.Update(id, bookToUpdate);
            }
            return validationResult;

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
                validationResults.Add(new ValidationError(ValidationConstants.IsTooLong, nameof(book.ISBN)));
            if (book.PublicationDate.Equals(null))
                validationResults.Add(new ValidationError(ValidationConstants.FieldIsRequired, nameof(book.PublicationDate)));


            return validationResults;


        }

        public Book? GetById(int id) =>  _bookRepository.GetById(id);
        public Book? GetByTitle(string title) => _bookRepository.GetByTitle(title);
        public List<Book> GetAll() => _bookRepository.GetAll();
        public void DeleteBook(int id) => _bookRepository.Delete(id);

        
    }
} 

