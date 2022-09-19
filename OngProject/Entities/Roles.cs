using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class Role : BaseEntity
    {
        public Role()
        {
            Users = new List<User>();
        }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }

}
