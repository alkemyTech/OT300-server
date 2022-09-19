using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class SocialMedia : BaseEntity
    {
       
        [Required]
        [DataType(DataType.Url)]
        public string FacebookUrl { get; set; }

        [Required]
        [DataType(DataType.Url)]
        public string InstagramUrl { get; set; }

        [Required]
        [DataType(DataType.Url)]
        public string LinkedInUrl { get; set; }
    }
}
