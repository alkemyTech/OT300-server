using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Collections.Generic;

namespace OngProject.Core.Mapper
{
    public static class MemberMapper
    {
        public static MembersDTO ToPublicDTO(this Member member)
        {
            var dto = new MembersDTO()
            {
                Name = member.Name,
                FacebookUrl = member.FacebookUrl,
                InstagramUrl = member.InstagramUrl,
                LinkedInUrl = member.LinkedInUrl,
                Image = member.Image,
                Description = member.Description,
            };
            return dto;
        }
    }
}
