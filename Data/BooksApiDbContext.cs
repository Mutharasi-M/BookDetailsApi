using BookDetailsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookDetailsApi.Data
{
    public class BooksApiDbContext : DbContext
    {
        public BooksApiDbContext(DbContextOptions options): base(options)
        { 
            
        }

        public DbSet<BookDetail> BookDetails { get; set; }
    }
}
