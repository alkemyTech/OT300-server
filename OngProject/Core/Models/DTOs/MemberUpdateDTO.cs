using System.IO;

namespace OngProject.Core.Models.DTOs
{
    public class MemberUpdateDTO
    {
        public string Name { get; set; }
        public string? FacebookUrl { get; set; }
        public string? InstagramUrl { get; set; }
        public string? LinkedInUrl { get; set; }
        internal Stream ImageFile { get; set; }
        public string? Description { get; set; }
    }
}
