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
        public string Description { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
    }
}
