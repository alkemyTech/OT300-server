using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Models.DTOs
{
    public class TestimonialDTO
    {
        [Required]
        public string Name { get; set; }
        internal Stream Image { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
