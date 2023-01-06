﻿using Intive.Core.Database;
using Intive.Core.Entities;
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

        public Book GetById(int id)
        {
            return _appDbContext.Books
                .Include(x => x.BookAuthors)
                .ThenInclude(x => x.Author)
                .FirstOrDefault(x => x.Id == id);
        }

        public Book GetByTitle(string title)
        {
            return _appDbContext.Books
                .Include(x => x.BookAuthors)
                .ThenInclude(x => x.Author)
                .FirstOrDefault(x => x.Title == title);
        }

        public IEnumerable<Book> SearchBook(string query)
        {
            var books = _appDbContext.Books
                .Include(x => x.BookAuthors)
                .ThenInclude(x => x.Author)
                .Where(x => x.Title.Contains(query) || x.Description.Contains(query));
            return books;
        }

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

        public void Delete(int id)
        {
            var bookToDelete = _appDbContext.Books.Find(id);

            if (bookToDelete != null) _appDbContext.Books.Remove(bookToDelete);

            _appDbContext.SaveChanges();
        }

        public void Create(Book entity)
        {
            _appDbContext.Books.Add(entity);
            _appDbContext.SaveChanges();
        }

        public List<Book> GetAll()
        {
            return _appDbContext.Books
                .Include(x => x.BookAuthors)
                .ThenInclude(x => x.Author)
                .OrderBy(x => x.Title)
                .ToList();
        }
    }
}
