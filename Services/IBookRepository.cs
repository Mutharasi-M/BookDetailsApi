using BookDetailsApi.Models;

namespace BookDetailsApi.Services
{
    public interface IBookRepository
    {
        Task<IEnumerable<BookDetail>> GetAllBookDetailsAsync();
        Task<BookDetail?> GetBookDetailsByIdAsync(int id);
        Task SaveBooksAsync(IEnumerable<BookDetail> books);
        Task<IEnumerable<BookDetail>?> UpdateBooksAsync(IEnumerable<BookDetail> books);
        Task<IEnumerable<BookDetail>?> DeleteBooksAsync(IList<int> ids);
        Task<IEnumerable<BookDetail>> GetBooksSortedByPublisherAsync();
        Task<IEnumerable<BookDetail>> GetBooksSortedByAuthorAsync();
        Task<IEnumerable<BookDetail>> GetBooksSortedByPublisherAsyncWithStoredProcedure();
        Task<IEnumerable<BookDetail>> GetBooksSortedByAuthorAsyncWithStoredProcedure();
        Task<decimal> GetTotalPriceAsync();

    }
}
