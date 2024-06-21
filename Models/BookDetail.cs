using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace BookDetailsApi.Models
{
    public class BookDetail
    {
        public int Id { get; set; }
        public string Publisher { get; set; }
        public string Title { get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorFirstName { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public DateTime PublicationDate { get; set; }
        public string? ContainerTitle { get; set; }
        public string? JournalTitle { get; set; }
        public int? VolumeNumber { get; set; }
        public string? PageRange { get; set; }
        public string? UrlOrDoi { get; set; }

        public string MLACitation => BuildMLACitation();
        public string ChicagoCitation => BuildChicagoCitation();

        private string BuildMLACitation()
        {
            string citation = $"{AuthorLastName}, {AuthorFirstName}. '{Title}'";

            if (!string.IsNullOrEmpty(ContainerTitle))
            {
                citation += $" {ContainerTitle},";
            }
            else
            {
                citation += ",";
            }

            citation += $" {Publisher}, {PublicationDate.Year}";

            if (!string.IsNullOrEmpty(PageRange))
            {
                citation += $", pp. {PageRange}.";
            }
            else
            {
                citation += ".";
            }

            return citation;
        }

        private string BuildChicagoCitation()
        {
            string citation = $"{AuthorLastName}, {AuthorFirstName}. {PublicationDate.Year}. '{Title}'";

            if (!string.IsNullOrEmpty(JournalTitle))
            {
                citation += $" {JournalTitle}";
            }

            if (VolumeNumber.HasValue)
            {
                citation += $", no. {VolumeNumber}";
            }

            citation += $" ({PublicationDate.ToString("MMMM yyyy", CultureInfo.InvariantCulture)})";

            if (!string.IsNullOrEmpty(PageRange))
            {
                citation += $": {PageRange}.";
            }
            else
            {
                citation += ".";
            }

            if (!string.IsNullOrEmpty(UrlOrDoi))
            {
                citation += $" {UrlOrDoi}";
            }

            return citation;
        }
    }
}
