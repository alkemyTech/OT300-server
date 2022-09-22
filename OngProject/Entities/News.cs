using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OngProject.Entities
{
    public class News : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Image { get; set; }
        
        [ForeignKey("Category")]
        public int IdCategory { get; set; }

    }
}
