using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class Slides : BaseEntity
    {
        [Required]

        public string ImageUrl { get; set; }

        [Required]

        public string Text { get; set; }

        [Required]
        public int Order { get; set; }

        [Required]
        public int OrganizationId { get; set; }
    }
}
