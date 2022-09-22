using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OngProject.Entities
{
    public class Member: BaseEntity
    { 
        //Id and TimeStamp come from BaseEntity

        [Required(ErrorMessage ="Name is required"), DataType(DataType.Text), MaxLength(50)]
        public string Name { get; set; }

        [DataType(DataType.Url)]
        public string? FacebookUrl { get; set; }

        [DataType(DataType.Url)]
        public string? InstagramUrl { get; set; }

        [DataType(DataType.Url)]
        public string? LinkedInUrl { get; set; }

        [Required(ErrorMessage = "An image is required")]
        public string Image { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string? Description { get; set; }
       
    }
}
