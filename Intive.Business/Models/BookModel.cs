using System.ComponentModel.DataAnnotations;

namespace Intive.Business.Models
{
    public class BookModel
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Rating { get; set; }

        [Required]
        [StringLength(13)]
        public string ISBN { get; set; }

        [Required]
        public DateTime PublicationDate { get; set; }

        [Required]
        public int AuthorId { get; set; }

        public string AuthorName { get; internal set; }
      
    }
}
