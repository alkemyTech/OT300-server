using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class UpdateCommentDTO
    {
        [Required]
        public string Body { get; set; }
    }
}