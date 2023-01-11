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
        /// <summary>
        /// Retrieve all books
        /// </summary>
        /// <returns>All books</returns>
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var allBooks = _bookService.GetAll();
            return Ok(allBooks);
        }

        // GET/books/id
        /// <summary>
        /// Get a book by id
        /// </summary>
        /// <param name="id">Id of the searched book</param>
        /// <returns>Searched book</returns>
        [HttpGet("id/{id}")]
        public IActionResult GetById(int id)
        {
            var book = _bookService.GetById(id);

            if (book == null)
                return NotFound();
            return Ok(book);
        }

        

        // GET/books/title
        /// <summary>
        /// Get a book by title
        /// </summary>
        /// <param name="title">Title of the searched book</param>
        /// <returns>Searched book</returns>
        [HttpGet("{title}")]
        public IActionResult GetByTitle(string title)
        {
            var book = _bookService.GetByTitle(title);

            if (book == null)
                return NotFound();
            return Ok(book);
        }

        // GET/books/search/query
        /// <summary>
        /// Searching books by query
        /// </summary>
        /// <param name="query">Searching query</param>
        /// <returns>Books containing searching query</returns>
        [HttpGet("search/{query}")]
        public IActionResult SearchBook(string query)
        {
            var book = _bookService.SearchBook(query);

            if (book == null)
                return NotFound();
            return Ok(book);
        }

        // POST/books/create
        /// <summary>
        /// Create a book
        /// </summary>
        /// <param name="book">Parameters of a new book</param>
        /// <returns></returns>
        [HttpPost("create")]
        public IActionResult Create(BookModel book)
        {
            _bookService.CreateBook(book);
            return Ok();
        }

        // PUT/books/update/id
        /// <summary>
        /// Update a book by id
        /// </summary>
        /// <param name="id">Id of a book to update</param>
        /// <param name="book">Book parameters to update</param>
        /// <returns></returns>
        [HttpPut("update/{id}")]
        public IActionResult Update(int id, BookModel book)
        {
            var bookToUpdate = _bookService.UpdateBook(id, book);
            return Ok(bookToUpdate);
        }

        // DELETE/books/delete/id
        /// <summary>
        /// Delete a book by id
        /// </summary>
        /// <param name="id">Id of a book to delete</param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _bookService.DeleteBook(id);
            return Ok();
        }


    }
}
