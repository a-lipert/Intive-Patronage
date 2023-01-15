using Intive.Core.Database;
using Intive.Core.Entities;
using Intive.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace Intive.Core.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _appDbContext;
        public BookRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        /// <summary>
        /// Gets book by id
        /// </summary>
        /// <param name="id">Book id</param>
        /// <returns>Book of corresponding id</returns>
        public Book GetById(int id)
        {
            return _appDbContext.Books
                .Include(x => x.BookAuthors)
                .ThenInclude(x => x.Author)
                .FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Gets book by title
        /// </summary>
        /// <param name="title"></param>
        /// <returns>Book of corresponding title</returns>
        public Book GetByTitle(string title)
        {
            return _appDbContext.Books
                .Include(x => x.BookAuthors)
                .ThenInclude(x => x.Author)
                .FirstOrDefault(x => x.Title == title);
        }

        /// <summary>
        /// Retrieves all searched books
        /// </summary>
        /// <param name="query">Searching query</param>
        /// <param name="orderBy">Order type</param>
        /// <param name="orderDescending"></param>
        /// <returns>Books containing searching query</returns>
        public IEnumerable<Book> SearchBook(string query, BookOrderBy orderBy, bool orderDescending)
        {
            var booksQuery = _appDbContext.Books
                .Include(x => x.BookAuthors)
                .ThenInclude(x => x.Author)
                .Where(x => x.Title.Contains(query) || x.Description.Contains(query) || x.Rating.ToString() == query || x.PublicationDate.ToString() == query);

            booksQuery = OrderBy(booksQuery, orderBy, orderDescending);
            return booksQuery.ToList();
        }

        /// <summary>
        /// Edits existing book 
        /// </summary>
        /// <param name="id">Id of a book to update</param>
        /// <param name="book">Book parameters to update</param>
        /// <returns></returns>
        public bool Update(int id, Book book)
        {
            var bookToUpdate = _appDbContext.Books.Find(id);

            if (bookToUpdate != null)
            {
                bookToUpdate.Title = book.Title;
                bookToUpdate.Description = book.Description;
                bookToUpdate.Rating = book.Rating;
                bookToUpdate.ISBN = book.ISBN;
                bookToUpdate.PublicationDate = book.PublicationDate;

                _appDbContext.SaveChanges();
                return true;
            };
            return false;
        }

        /// <summary>
        /// Removes book from db by id 
        /// </summary>
        /// <param name="id">Id of a book to delete</param>
        public void Delete(int id)
        {
            var bookToDelete = _appDbContext.Books.Find(id);

            if (bookToDelete != null) 
                _appDbContext.Books.Remove(bookToDelete);

            _appDbContext.SaveChanges();
        }

        /// <summary>
        /// Creates book entity and adds to db
        /// </summary>
        /// <param name="entity">Parameters of a new book entity</param>
        public void Create(Book entity)
        {
            _appDbContext.Books.Add(entity);
            _appDbContext.SaveChanges();
        }

        /// <summary>
        /// Retrieves all books from db
        /// </summary>
        /// <returns>List of all books</returns>
        public List<Book> GetAll()
        {
            return _appDbContext.Books
                .Include(x => x.BookAuthors)
                .ThenInclude(x => x.Author)
                .OrderBy(x => x.Title)
                .ToList();
        }

        /// <summary>
        /// Checks if there is any book corresponding to the id in the db
        /// </summary>
        /// <param name="bookId">Book id</param>
        /// <returns>True if author with the id exists</returns>
        public bool BookExists(int bookId)
        {
            return _appDbContext.Books.Any(b => b.Id == bookId);
        }


        /// <summary>
        /// Orders book according to the chosen parameter
        /// </summary>
        /// <param name="query">Retrieved books</param>
        /// <param name="orderBy">Order by parameter</param>
        /// <param name="orderDescending">True for descending order</param>
        /// <returns>Sorted books</returns>
        private IQueryable<Book> OrderBy(IQueryable<Book> query, BookOrderBy orderBy, bool orderDescending)
        {
            switch (orderBy)
            {
                case BookOrderBy.AuthorName: 
                    return orderDescending ? query.OrderByDescending(x => x.BookAuthors.FirstOrDefault().Author.LastName)
                        : query.OrderBy(x => x.BookAuthors.FirstOrDefault().Author.LastName);
                case BookOrderBy.Title: 
                    return orderDescending ? query.OrderByDescending(x => x.Title) : query.OrderBy(x => x.Title);
                case BookOrderBy.Rating: 
                    return orderDescending ? query.OrderByDescending(x => x.Rating) : query.OrderBy(x => x.Rating);
                default:
                    return query;
            }
        }

       
    }
}
