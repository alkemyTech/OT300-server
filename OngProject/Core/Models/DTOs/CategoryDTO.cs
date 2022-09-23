using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class CategoryDTO
    {
        [Required]
        public string Name { get; set; }

    }
}
