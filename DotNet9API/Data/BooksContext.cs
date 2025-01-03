using Microsoft.EntityFrameworkCore;

namespace DotNet9API.Data
{
    public class BooksContext : DbContext
    {
        public BooksContext(DbContextOptions options) : base(options)
        {
        }
    }
}
