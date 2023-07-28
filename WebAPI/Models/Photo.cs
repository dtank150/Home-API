using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Photo : BaseEntity
    {
        [Required]
        public string PublicId { get; set; }
       [Required]
        public string ImageUrl { get; set; }
        public bool IsPrimary { get; set; }
        public int PropertyId { get; set; }
        public Property Property { get; set; }

    }
}