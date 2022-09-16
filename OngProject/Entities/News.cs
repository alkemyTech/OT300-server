﻿using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class News : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Image { get; set; }

        public int IdCategory { get; set; }

    }
}