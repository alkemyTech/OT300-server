﻿using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class Register : BaseEntity
    {

        [Required]
        [MinLength(5)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(5)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8), MaxLength(16)]
        public string Password { get; set; }
    }
}
