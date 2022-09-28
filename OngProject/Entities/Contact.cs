using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{

    public class Contact : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public int Phone { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(200)]
        public string Message { get; set; }
    }
}