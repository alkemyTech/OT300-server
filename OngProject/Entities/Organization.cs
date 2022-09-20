using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class Organization : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Img { get; set; }
        public string Adress { get; set; }
        public int PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Url)]
        public string FacebookUrl { get; set; }

        [Required]
        [DataType(DataType.Url)]
        public string InstagramUrl { get; set; }

        [Required]
        [DataType(DataType.Url)]
        public string LinkedInUrl { get; set; }

        [Required]
        public string WelcomeText { get; set; }
        public string AboutUsText { get; set; }
    }
}