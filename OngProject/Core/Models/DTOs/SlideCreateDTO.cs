using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace OngProject.Core.Models.DTOs
{
    public class SlideCreateDTO
    {
        [Required]
        public IFormFile ImageStream { get; set; }

        [Required]
        [MaxLength(255), DataType(DataType.MultilineText)]
        public string Text { get; set; }

        [Required]
        public int Order { get; set; }

        [Required]
        [ForeignKey("Organization")]
        public int OrganizationId { get; set; }
    }
}