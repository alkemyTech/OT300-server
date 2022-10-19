using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using OngProject.Entities;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace OngProject.Core.Models.DTOs
{
    public class MembersDTO
    {
        /// <summary>
        /// The Name of the member
        /// </summary>
        /// <example>Adrian</example>
        [Required(ErrorMessage = "Name is required"), MaxLength(50)]
        public string Name { get; set; }
        /// <summary>
        /// facebook link
        /// </summary>
        /// <example>http://facebook.com/adrian</example>
        public string? FacebookUrl { get; set; }
        /// <summary>
        /// Instagram link
        /// </summary>
        /// <example>http://Instagram.com/adrian</example>
        public string? InstagramUrl { get; set; }
        /// <summary>
        /// LinkedIn link
        /// </summary>
        /// <example>http://linkedIn.com/adrian</example>
        public string? LinkedInUrl { get; set; }
        [Required(ErrorMessage = "An image is required")]
        internal Stream Image { get; set; }

        /// <summary>
        /// Some Decription
        /// </summary>
        /// <example>Adrian is one of our founders</example>
        [MaxLength(255)]
        public string? Description { get; set; }
    }
}
