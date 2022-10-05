using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OngProject.Core.Models.DTOs
{
    public class CommentDTO
    {
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
