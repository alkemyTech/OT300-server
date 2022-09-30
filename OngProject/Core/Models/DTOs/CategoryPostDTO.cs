using System.IO;

namespace OngProject.Core.Models.DTOs
{
    public class CategoryPostDTO : CategoryDTO
    {
        public string Description { get; set; }
        internal Stream File { get; set; }
    }
}
