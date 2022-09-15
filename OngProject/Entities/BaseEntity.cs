using System;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class BaseEntity
    {
        [Required]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime LastEditedAt { get; set; }

    }
}