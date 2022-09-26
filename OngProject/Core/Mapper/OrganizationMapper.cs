using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Collections.Generic;

namespace OngProject.Core.Mapper
{
    public static class OrganizationMapper
    {
        /// <summary>
        /// Get public organization info
        /// </summary>
        /// <param name="org"></param>
        /// <returns>DTO including Name.Image,Address, PhoneNumber</returns>
        public static OrganizationPublicDTO ToPublicDTO(this Organization org)
        {
            var dto = new OrganizationPublicDTO()
            {
                Name = org.Name,
                Img = org.Img,
                Adress = org.Adress,
                PhoneNumber = org.PhoneNumber,
                FacebookUrl = org.FacebookUrl,
                InstagramUrl = org.InstagramUrl,
                LinkedInUrl = org.LinkedInUrl
            };
            return dto;
        }
    }
}
