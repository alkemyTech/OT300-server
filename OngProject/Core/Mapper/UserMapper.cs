using Microsoft.AspNetCore.Http;
using Microsoft.Win32;
using OngProject.Core.Helper;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Runtime.CompilerServices;

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

        public static User PatchToEntity(this User user, UserPatchDTO patchDTO)
        {
            user.FirstName = patchDTO.FirstName ?? user.FirstName;
            user.LastName = patchDTO.LastName ?? user.LastName;
            user.Photo = patchDTO.Photo ?? user.Photo;
            user.Email = patchDTO.Email ?? user.Email ;
            user.Password = AuthHelper.EncryptPassword(patchDTO.Password) ?? user.Password;

            return user;
        }
    }
}
