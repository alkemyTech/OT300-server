using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class RoleDTO
    {
        [Required]
        public string Name { get; set; }
        [DataType(DataType.Text)]
        public string Description { get; set; }
    }
}