using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace OngProject.Core.Models.DTOs
{
    public class NewsDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Image { get; set; }
        [Required, ForeignKey("Category")]
        public int IdCategory { get; set; }
    }

    public class NewsFullDTO : NewsDTO
    {
        public int Id { get; set; }
    }

    public class NewsPostDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Content { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid value {0}")]
        public int IdCategory { get; set; }

        internal Stream ImageFile { get; set; }

    }

    public class NewsPutDTO
    {
        public string Name { get; set; }
        public string Content { get; set; }
        internal Stream ImageFile { get; set;}
        [Required]
        public int IdCategory { get; set;}

    }
}


/*
 
 Campos:
id: INTEGER NOT NULL AUTO_INCREMENT
name: VARCHAR NOT NULL
content: TEXT NOT NULL
image: VARCHAR NOT NULL
categoryId: Clave foranea hacia ID de Categories
timestamps y softDeletes
 */