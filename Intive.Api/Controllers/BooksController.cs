using Microsoft.AspNetCore.Mvc;
using Intive.Business.Services;
using Intive.Business.Models;

namespace Intive.Api.Controllers
{
    [ApiController]
    [Route("books")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET/books
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var allBooks = _bookService.GetAll();
            return Ok(allBooks);
        }

        // GET/books/id
        [HttpGet("id/{id}")]
        public IActionResult GetById(int id)
        {
            var book = _bookService.GetById(id);

            if (book == null)
                return NotFound();
            return Ok(book);
        }

        

        // GET/books/title
        [HttpGet("{title}")]
        public IActionResult GetByTitle(string title)
        {
            var book = _bookService.GetByTitle(title);

            if (book == null)
                return NotFound();
            return Ok(book);
        }

        // GET/books/search/query
        [HttpGet("search/{query}")]
        public IActionResult SearchBook(string query)
        {
            var book = _bookService.SearchBook(query);

            if (book == null)
                return NotFound();
            return Ok(book);
        }

        // POST/books/create
        [HttpPost("create")]
        public IActionResult Create(BookModel book)
        {
            _bookService.CreateBook(book);
            return Ok();
        }

        // PUT/books/update/id
        [HttpPut("update/{id}")]
        public IActionResult Update(int id, BookModel book)
        {
            var bookToUpdate = _bookService.UpdateBook(id, book);
            return Ok(bookToUpdate);
        }

        // DELETE/books/delete/id
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _bookService.DeleteBook(id);
            return Ok();
        }


    }
}
