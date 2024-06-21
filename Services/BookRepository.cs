using BookDetailsApi.Data;
using BookDetailsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookDetailsApi.Services
{
    public class BookRepository : IBookRepository
    {
        private readonly BooksApiDbContext _dbContext;

        public BookRepository(BooksApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<BookDetail>> GetAllBookDetailsAsync()
        {
            return await _dbContext.BookDetails.ToListAsync();
        }

        public async Task<IEnumerable<BookDetail>> GetBooksSortedByPublisherAsync()
        {
            return await _dbContext.BookDetails
                .OrderBy(x => x.Publisher)
                .ThenBy(x => x.AuthorLastName)
                .ThenBy(x => x.AuthorFirstName)
                .ThenBy(x => x.Title)
                .ToListAsync();
        }

        public async Task<IEnumerable<BookDetail>> GetBooksSortedByAuthorAsync()
        {
            return await _dbContext.BookDetails
                .OrderBy(x => x.AuthorLastName)
                .ThenBy(x => x.AuthorFirstName)
                .ThenBy(x => x.Title)
                .ToListAsync();
        }

        public async Task<IEnumerable<BookDetail>> GetBooksSortedByPublisherAsyncWithStoredProcedure()
        {
            return await _dbContext.BookDetails.FromSqlRaw("exec getbookssortedbypublisher").ToListAsync();
        }

        public async Task<IEnumerable<BookDetail>> GetBooksSortedByAuthorAsyncWithStoredProcedure()
        {
            return await _dbContext.BookDetails.FromSqlRaw("exec getbookssortedbyauthor").ToListAsync();
        }

        public async Task<decimal> GetTotalPriceAsync()
        {
            return await _dbContext.BookDetails.SumAsync(b => b.Price);
        }

        public async Task SaveBooksAsync(IEnumerable<BookDetail> books)
        {
            await _dbContext.BookDetails.AddRangeAsync(books);
            await _dbContext.SaveChangesAsync();
        }        
    }
}
