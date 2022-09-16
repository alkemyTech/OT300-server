using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class Role : BaseEntity
    {
        public Role()
        {
            User = new List<Users>();
        }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }

}
