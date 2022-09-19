using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OngProject.Entities
{
    public class Activity : BaseEntity
    {
        [Required]
        [Column(TypeName = "varchar()")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "varchar(max)")]
        public string Content { get; set; }

        [Required]
        [Column(TypeName = "varchar()")]
        public string Image { get; set; }
    }
}
