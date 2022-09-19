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
        public string WelcomeText { get; set; }
        public string AboutUsText { get; set; }
    }
}