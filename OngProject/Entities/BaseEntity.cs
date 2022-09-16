using System;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime LastEditedAt { get; set; }

    }
}