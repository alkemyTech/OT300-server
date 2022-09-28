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
                Email = user.Email,
                Photo = user.Photo,
                RoleId = user.RoleId                
            };
            return userDTO;
        }

    }
}
