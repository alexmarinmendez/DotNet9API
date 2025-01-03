using DotNet9API.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNet9API.Data
{
    public class BooksContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public BooksContext(DbContextOptions<BooksContext> options) : base(options)
        {
        }
    }
}
