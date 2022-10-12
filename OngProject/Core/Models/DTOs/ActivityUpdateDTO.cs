using System.IO;

namespace OngProject.Core.Models.DTOs
{
    public class ActivityUpdateDTO
    {
        public string Name { get; set; }
        public string Content { get; set; }
        internal Stream ImageFile { get; set; }
    }
}
