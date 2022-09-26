using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class OrganizationPublicDTO
    {
        public string Name { get; set; }
        public string Img { get; set; }
        public string Adress { get; set; }
        public int PhoneNumber { get; set; }
        public string FacebookUrl { get; set; }
        public string LinkedInUrl { get; set; }
        public string InstagramUrl { get; set; }
    }
}
