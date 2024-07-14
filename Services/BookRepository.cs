using BookDetailsApi.Data;
using BookDetailsApi.Models;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace BookDetailsApi.Services
{
    public class BookRepository : IBookRepository
    {
        private readonly BooksApiDbContext _dbContext;
        private readonly ILogger<BookRepository> _logger;

        public BookRepository(BooksApiDbContext dbContext, ILogger<BookRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<BookDetail>> GetAllBookDetailsAsync()
        {
            try
            {
                return await _dbContext.BookDetails.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all book details.");
                throw;
            }
        }

        public async Task<BookDetail?> GetBookDetailsByIdAsync(int id)
        {
            try
            {
                return await _dbContext.BookDetails.Where(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting book details for Id: {id}");
                throw;
            }
        }

        public async Task SaveBooksAsync(IEnumerable<BookDetail> books)
        {
            if (books == null || !books.Any())
            {
                throw new ArgumentException("Books collection is null or empty.", nameof(books));
            }

            try
            {
                await _dbContext.BulkInsertAsync(books.ToList());
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving books.");
                throw;
            }
        }
        public async Task<IEnumerable<BookDetail>?> UpdateBooksAsync(IEnumerable<BookDetail> books)
        {
            if (books == null || !books.Any())
            {
                throw new ArgumentException("Books collection is null or empty.", nameof(books));
            }

            try
            {
                if (books != null && books.Any())
                {
                    await _dbContext.BulkInsertOrUpdateAsync(books);
                    await _dbContext.SaveChangesAsync();
                }

                return books;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while Updating a book.");
                throw;
            }
        }

        public async Task<IEnumerable<BookDetail>?> DeleteBooksAsync(IList<int> ids)
        {
            try
            {
                var books = await _dbContext.BookDetails.Where(x => ids.Contains(x.Id)).ToListAsync();
                if (books != null && books.Any())
                {
                    await _dbContext.BulkDeleteAsync(books);
                    await _dbContext.SaveChangesAsync();                    
                }

                return books;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting book with Ids: {ids}");
                throw;
            }
        }

        public async Task<IEnumerable<BookDetail>> GetBooksSortedByPublisherAsync()
        {
            try
            {
                return await _dbContext.BookDetails
                    .OrderBy(x => x.Publisher)
                    .ThenBy(x => x.AuthorLastName)
                    .ThenBy(x => x.AuthorFirstName)
                    .ThenBy(x => x.Title)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting books sorted by publisher.");
                throw;
            }
        }

        public async Task<IEnumerable<BookDetail>> GetBooksSortedByAuthorAsync()
        {
            try
            {
                return await _dbContext.BookDetails
                    .OrderBy(x => x.AuthorLastName)
                    .ThenBy(x => x.AuthorFirstName)
                    .ThenBy(x => x.Title)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting books sorted by author.");
                throw;
            }
        }

        public async Task<IEnumerable<BookDetail>> GetBooksSortedByPublisherAsyncWithStoredProcedure()
        {
            try
            {
                return await _dbContext.BookDetails.FromSqlRaw("exec getbookssortedbypublisher").ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting books sorted by publisher with stored procedure.");
                throw;
            }
        }

        public async Task<IEnumerable<BookDetail>> GetBooksSortedByAuthorAsyncWithStoredProcedure()
        {
            try
            {
                return await _dbContext.BookDetails.FromSqlRaw("exec getbookssortedbyauthor").ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting books sorted by author with stored procedure.");
                throw;
            }
        }

        public async Task<decimal> GetTotalPriceAsync()
        {
            try
            {
                return await _dbContext.BookDetails.SumAsync(b => b.Price);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while calculating the total price of books.");
                throw;
            }
        }
    }
}
