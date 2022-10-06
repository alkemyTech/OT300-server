using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace OngProject.Core.Models.DTOs
{
    public class OrganizationPublicDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public  string Img { get; set; }
        internal Stream ImageStream { get; set; }

        public string Adress { get; set; }
        public Nullable<int> PhoneNumber { get; set; }

        [Url(ErrorMessage = "A valid URL was excepected.")]
        [Required(AllowEmptyStrings =true)]
        public string FacebookUrl { get; set; }
        [Required]
        [Url(ErrorMessage = "A valid URL was excepected.")]
        public string LinkedInUrl { get; set; }
        [Required]
        [Url(ErrorMessage = "A valid URL was excepected.")]
        public string InstagramUrl { get; set; }
    }


    public class OrganizationPostPublicDTO : OrganizationPublicDTO
    {
        internal new string Img { get; set; }

    }
}
