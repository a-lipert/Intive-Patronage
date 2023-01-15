using Intive.Business.Helpers;
using Intive.Business.Models;
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

        /// <summary>
        /// Creates author
        /// </summary>
        /// <param name="author">Parameters of a new author</param>
        /// <returns>List of validation errors</returns>
        public List<ValidationError> CreateAuthor(AuthorModel author)
        {
            var validationResult = IsValid(author);

            if (!validationResult.Any())
            {
                var authorToCreate = author.ToAuthorEntity();
                _authorRepository.Create(authorToCreate);
            }

            return validationResult;
        }

        /// <summary>
        /// Retrieves all authors
        /// </summary>
        /// <returns>All authors</returns>
        public List<AuthorModel> GetAll()
        {
            var authors = _authorRepository.GetAll();
            
            return authors.Select(x => x.ToAuthorModel()).ToList();
        }

        /// <summary>
        /// Gets author by name
        /// </summary>
        /// <param name="name">First or last name of the author</param>
        /// <returns>Author model</returns>
        /// <exception cref="ArgumentNullOrEmptyException">Thrown if name property is null or empty</exception>
        public AuthorModel GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullOrEmptyException("book");
            }

            var author = _authorRepository.GetByName(name);
            if (author == null)
            {
                return null;
            }

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
