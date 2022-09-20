using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OngProject.Entities
{
    public class Activity : BaseEntity
    {
        [Required]
        [Column(TypeName = "varchar(MAX)")]
        [MaxLength]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "TEXT")]
        public string Content { get; set; }

        [Required]
        [Column(TypeName = "varchar(MAX)")]
        public string Image { get; set; }
    }
}
