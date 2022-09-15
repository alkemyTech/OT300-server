using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class Role:BaseEntity
    {
        public Role()
        {
            Roles = new List<Role>();
        }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
