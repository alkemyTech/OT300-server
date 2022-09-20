using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace OngProject.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    public class User : BaseEntity
    {
        //PK from BaseEntity
        [Required]
        [Column(TypeName = "varchar(MAX)")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "varchar(MAX)")]
        public string LastName { get; set; }

        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string Photo { get; set; }

        public Role Role { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
    }
}
