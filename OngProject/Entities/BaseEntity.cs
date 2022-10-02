using System;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset LastEditedAt { get; set; }

    }
}