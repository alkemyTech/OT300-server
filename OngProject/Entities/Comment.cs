using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OngProject.Entities
{
    public class Comment : BaseEntity
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
