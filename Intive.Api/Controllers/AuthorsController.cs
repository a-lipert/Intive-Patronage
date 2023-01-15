using Intive.Business.Models;
using Intive.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Intive.Api.Controllers
{
    [ApiController]
    [Route("authors")]
    public class AuthorsController: ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService; 
        }

        // GET/authors
        /// <summary>
        /// Retrieves all author
        /// </summary>
        /// <returns>All authors</returns>
        [HttpGet]
        public IActionResult GetAllAuthors()
        {
            var allAuthors = _authorService.GetAll();
            return Ok(allAuthors);
        }

        // POST/authors/create
        /// <summary>
        /// Creates an author
        /// </summary>
        /// <param name="author">Parameters of a new author</param>
        /// <returns></returns>
        [HttpPost("create")]
        public IActionResult CreateAuthor(AuthorModel author)
        {
            _authorService.CreateAuthor(author);
            return Ok();
        }


        // GET/authors/name
        /// <summary>
        /// Gets an author by first or last name
        /// </summary>
        /// <param name="name">First or Last Name of the searched author</param>
        /// <returns>Searched author</returns>
        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var authorByName = _authorService.GetByName(name);

            if (authorByName == null)
                return NotFound();
            return Ok(authorByName);
        }


    }
}
