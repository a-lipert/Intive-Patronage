using Intive.Business.Helpers;
using Intive.Business.Models;
using Intive.Core.Entities;
using Intive.Core.Repository;

namespace Intive.Business.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public List<ValidationError> CreateAuthor(AuthorModel author)
        {
            var validationResult = IsValid(author);

            if (validationResult.Any()) return validationResult;

            else 
            {
                var authorToCreate = author.ToAuthorEntity();
                _authorRepository.Create(authorToCreate);
            }

            return validationResult;
        }

        public List<AuthorModel> GetAll()
        {
            var authors = _authorRepository.GetAll();
            
            return authors.Select(x => x.ToAuthorModel()).ToList();
        }

        public AuthorModel GetByName(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Argument needs a value");
            var author = _authorRepository.GetByName(name);
            if (author == null) throw new ArgumentNullException("name");
            return author.ToAuthorModel();
        }

        private static List<ValidationError> IsValid(AuthorModel author)
        {
            var validationResults = new List<ValidationError>();

            if (string.IsNullOrEmpty(author.FirstName))
                validationResults.Add(new ValidationError(ValidationConstants.FieldIsRequired, nameof(author.FirstName)));
            if (author.FirstName.Length > 50)
                validationResults.Add(new ValidationError(ValidationConstants.IsTooLong, nameof(author.FirstName)));
            if (string.IsNullOrEmpty(author.LastName))
                validationResults.Add(new ValidationError(ValidationConstants.FieldIsRequired, nameof(author.LastName)));
            if (author.LastName.Length > 50)
                validationResults.Add(new ValidationError(ValidationConstants.IsTooLong, nameof(author.LastName)));
            if (author.BirthDate.Equals(DateTime.MinValue))
                validationResults.Add(new ValidationError(ValidationConstants.FieldIsRequired, nameof(author.BirthDate)));

            return validationResults;
        }

        

      

    }
}
