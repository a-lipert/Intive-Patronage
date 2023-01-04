using Intive.Core.Entities;
using Intive.Core.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Intive.Core.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>()
                .HasOne(b => b.Book)
                .WithMany(ba => ba.BookAuthors)
                .HasForeignKey(b => b.BookId);

            modelBuilder.Entity<BookAuthor>()
                .HasOne(a => a.Author)
                .WithMany(ba => ba.BookAuthors)
                .HasForeignKey(a => a.AuthorId);

            //value converter
             
            var converter = new ValueConverter<Gender, bool>(
            g => g == Gender.Male ? false : true,
            g => (Gender)Enum.ToObject(typeof(Gender), g));

            modelBuilder
                .Entity<Author>()
                .Property(p => p.Gender)
                .HasConversion(converter);
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BooksAuthors { get; set; }
    }
}
