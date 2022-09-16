using System.ComponentModel.DataAnnotations;


namespace OngProject.Entities
{
    public class Members: BaseEntity
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

        //Even when description isn't required it's assign a max length to be really specific
        [MaxLength(255)]
        public string? Description { get; set; }
       
    }
}
