using Intive.Business.Models;
using Intive.Business.Services;
using Intive.Core.Entities;
using Intive.Core.Repository;
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
        [HttpGet]
        public ActionResult GetAllAuthors()
        {
            var allAuthors = _authorService.GetAll();
            return Ok(allAuthors);
        }

        // POST/authors/create
        [HttpPost("create")]
        public ActionResult CreateAuthor(AuthorModel author)
        {
            _authorService.CreateAuthor(author);
            return Ok();
        }


        // GET/authors/name
        [HttpGet("{name}")]
        public ActionResult GetByName(string name)
        {
            var authorByName = _authorService.GetByName(name);

            if (authorByName == null)
                return NotFound();
            return Ok(authorByName);
        }


    }
}
