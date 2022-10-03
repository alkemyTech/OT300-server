using Microsoft.AspNetCore.Http;
using Microsoft.Win32;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;

namespace OngProject.Core.Mapper
{
    public static class UserMapper
    {

        public static UserGetDTO ToUserDTO (this User user)
        {
            UserGetDTO userDTO = new UserGetDTO()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Photo = user.Photo,
                RoleId = user.RoleId                
            };
            return userDTO;
        }

        public static UserTokenDTO ToUserTokenDTO(this User user)
        {
	    UserTokenDTO userTokenDTO = new UserTokenDTO
            {
                Id = user.Id,
		Email = user.Email,
		RoleId = user.RoleId
	     };

            return userTokenDTO;
	}
    }
}
