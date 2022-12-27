using Intive.Core.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Intive.Business.Models
{
    public class AuthorModel
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public Gender Gender { get; set; }
    }
}
