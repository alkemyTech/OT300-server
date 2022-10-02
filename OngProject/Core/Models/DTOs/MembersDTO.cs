using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace OngProject.Core.Models.DTOs
{
    public class MembersDTO
    {
        [Required(ErrorMessage ="Name is required"),MaxLength(50)]
        public string Name { get; set; }
        public string? FacebookUrl { get; set; }
        public string? InstagramUrl { get; set; }
        public string? LinkedInUrl { get; set; }
        [Required(ErrorMessage = "An image is required")]
        internal Stream Image { get; set; }
        [MaxLength(255)]
        public string? Description { get; set; }
        
    }
}
