using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OngProject.Entities
{
    public class Slide:BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(255)]
        public string Text { get; set; }

        [Required]
        public int Order { get; set; }

        [Required]
        [ForeignKey("Organization")]
        public int OrganizationId { get; set; }
    }
}
