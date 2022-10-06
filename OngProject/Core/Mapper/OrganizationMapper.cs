using OngProject.Core.Models.DTOs;
using OngProject.Entities;

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

        public static Organization ToEntity(this OrganizationPublicDTO org)
        {
            var entity = new Organization();
            entity.Name = org.Name ?? string.Empty;
            entity.Img = org.Img ?? string.Empty;
            entity.Adress = org.Adress;
            entity.PhoneNumber = org.PhoneNumber.HasValue ? org.PhoneNumber.Value : 0;
            entity.FacebookUrl = org.FacebookUrl ?? string.Empty;
            entity.InstagramUrl = org.InstagramUrl ?? string.Empty;
            entity.LinkedInUrl = org.LinkedInUrl ?? string.Empty;



            return entity;

        }

        public static Organization SetNewValues(this Organization entity, OrganizationPublicDTO dto)
        {
            entity.Name = dto.Name ?? string.Empty;
            entity.Img = dto.Img ?? string.Empty;
            entity.Adress = dto.Adress;
            entity.PhoneNumber = dto.PhoneNumber.HasValue ? dto.PhoneNumber.Value : 0;
            entity.FacebookUrl = dto.FacebookUrl ?? string.Empty;
            entity.InstagramUrl = dto.InstagramUrl ?? string.Empty;
            entity.LinkedInUrl = dto.LinkedInUrl ?? string.Empty;
            return entity;

        }
    }
}
