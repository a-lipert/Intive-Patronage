using Intive.Core.Database;
using Intive.Core.Entities;
using Intive.Core.Repository;
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
        public ActionResult GetAllBooks()
        {
            var allBooks = _bookService.GetAll();
            return Ok(allBooks);
        }

        // GET/books/id
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var book = _bookService.GetById(id);

            if (book == null)
                return NotFound();
            return Ok(book);
        }

        // POST/books/create
        [HttpPost("create")]
        public ActionResult Create(BookModel book)
        {
            _bookService.CreateBook(book);
            return Ok();
        }

        // PUT/books/update/id
        [HttpPut("update/{id}")]
        public ActionResult Update(int id, BookModel book)
        {
            var bookToUpdate = _bookService.UpdateBook(id, book);
            return Ok(bookToUpdate);
        }

        // DELETE/books/delete/id
        [HttpDelete("delete/{id}")]
        public ActionResult Delete(int id)
        {
            _bookService.DeleteBook(id);
            return Ok();
        }


    }
}
