using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace OngProject.Entities
{
    public class User : BaseEntity
    {
        //PK from BaseEntity
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Photo { get; set; }

        public Role Role { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
    }
}
