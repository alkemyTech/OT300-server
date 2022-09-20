using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OngProject.Core.Models.DTOs
{
    public class NewsDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Image { get; set; }
        [Required, ForeignKey("Category")]
        public int IdCategory { get; set; }
    }
}
