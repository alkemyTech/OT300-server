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
                //Image = member.Image,
                Description = member.Description,
            };
            return dto;
        }

        public static Member DtoToMember (this MembersDTO membersDTO)
        {
            Member member = new Member()
            {
                Name = membersDTO.Name,
                Description = membersDTO.Description,
                FacebookUrl = membersDTO.FacebookUrl,
                LinkedInUrl = membersDTO.LinkedInUrl,
                InstagramUrl = membersDTO.InstagramUrl
            };


            return member;
        }

        public static Member UpdateDtoToMember(this Member member, MemberUpdateDTO memberUpdateDTO)
        {

            member.Name = memberUpdateDTO.Name;
            member.FacebookUrl = memberUpdateDTO.FacebookUrl;
            member.Description = memberUpdateDTO.Description;
            member.LinkedInUrl = memberUpdateDTO.LinkedInUrl;
            member.InstagramUrl = memberUpdateDTO.InstagramUrl;
            
            return member;
        }
    }
}
