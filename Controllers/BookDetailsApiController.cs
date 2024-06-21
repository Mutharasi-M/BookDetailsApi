using Microsoft.AspNetCore.Mvc;
using BookDetailsApi.Models;
using BookDetailsApi.Services;
using System.Globalization;

namespace BookDetailsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookDetailsApiController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookDetailsApiController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpPost("initialize-sample-data")]
        public async Task<IActionResult> InitializeSampleData()
        {
            var basicBooksDetails = await _bookRepository.GetAllBookDetailsAsync();

            // Check if there are already books in the repository
            if (!basicBooksDetails.Any())
            {
                // If no books exist, add sample books
                var sampleBooks = new List<BookDetail>
                {
                    new BookDetail
                    {
                        Publisher = "A Penguin Random House",
                        Title = "A Mystery of the Talking Wombat",
                        AuthorLastName = "Smith",
                        AuthorFirstName = "Jane",
                        Price = 24.99M,
                        PublicationDate = DateTime.ParseExact("2023-05-15", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                        JournalTitle = "Psychic Research Journal",
                        ContainerTitle = "Studies in Animal Communication",
                        VolumeNumber = 5,
                        PageRange = "112-115",
                        UrlOrDoi = "https://doi.org/10.xxxx/yyyyyy"
                    },
                    new BookDetail
                    {
                        Publisher = "Oxford University Press",
                        Title = "Jealousy in Shakespeare: A Psychological Analysis",
                        AuthorLastName = "Jones",
                        AuthorFirstName = "David",
                        Price = 39.95M,
                        PublicationDate = DateTime.ParseExact("2022-11-10", "yyyy-MM-dd", CultureInfo.InvariantCulture)
                    }
                };

                await _bookRepository.SaveBooksAsync(sampleBooks);
                return Ok("Sample data initialized successfully.");
            }

            return Ok("Sample data already exists.");
        }


        [HttpGet("sorted-by-publisher")]
        public async Task<IActionResult> GetBooksSortedByPublisher()
        {
            var books = await _bookRepository.GetBooksSortedByPublisherAsync();
            return Ok(books);
        }

        [HttpGet("sorted-by-author")]
        public async Task<IActionResult> GetBooksSortedByAuthor()
        {
            var books = await _bookRepository.GetBooksSortedByAuthorAsync();
            return Ok(books);
        }

        [HttpGet("sorted-by-publisher-with-stored-procedure")]
        public async Task<IActionResult> GetBooksSortedByPublisherWithStoredProcedure()
        {
            var books = await _bookRepository.GetBooksSortedByPublisherAsyncWithStoredProcedure();
            return Ok(books);
        }

        [HttpGet("sorted-by-author-with-stored-procedure")]
        public async Task<IActionResult> GetBooksSortedByAuthorWithStoredProcedure()
        {
            var books = await _bookRepository.GetBooksSortedByAuthorAsyncWithStoredProcedure();
            return Ok(books);
        }

        [HttpGet("total-price")]
        public async Task<IActionResult> GetTotalPrice()
        {
            var totalPrice = await _bookRepository.GetTotalPriceAsync();
            return Ok(totalPrice);
        }

        [HttpPost("save-list")]
        public async Task<IActionResult> SaveBooks([FromBody] IEnumerable<BookDetail> books)
        {
            await _bookRepository.SaveBooksAsync(books);
            return Ok();
        }
    }
}
