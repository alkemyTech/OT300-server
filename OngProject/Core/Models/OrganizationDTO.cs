using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class OrganizationDTO
    {
        public string Name { get; set; }
        public string Img { get; set; }
        public string Adress { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public string WelcomeText { get; set; }
        public string AboutUsText { get; set; }
    }
}
