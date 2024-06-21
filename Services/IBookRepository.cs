using BookDetailsApi.Models;

namespace BookDetailsApi.Services
{
    public interface IBookRepository
    {
        Task<IEnumerable<BookDetail>> GetAllBookDetailsAsync();
        Task<IEnumerable<BookDetail>> GetBooksSortedByPublisherAsync();
        Task<IEnumerable<BookDetail>> GetBooksSortedByAuthorAsync();
        Task<IEnumerable<BookDetail>> GetBooksSortedByPublisherAsyncWithStoredProcedure();
        Task<IEnumerable<BookDetail>> GetBooksSortedByAuthorAsyncWithStoredProcedure();
        Task<decimal> GetTotalPriceAsync();
        Task SaveBooksAsync(IEnumerable<BookDetail> books);
    }
}
