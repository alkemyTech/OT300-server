using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace OngProject.Core.Models.DTOs
{
    public class ActivityDTO
    {
        [Required(ErrorMessage = "The Name is required"), MaxLength(100)]
        [Column(TypeName = "varchar(MAX)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Content is required"), MaxLength(250)]
        [Column(TypeName = "TEXT")]
        public string Content { get; set; }

        [Required(ErrorMessage = "An image is required")]
        internal Stream ImageFile { get; set; }
    }
}
