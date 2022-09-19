using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class Testimonial: BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Image { get; set; }
        public string Content { get; set; }


    }
}
