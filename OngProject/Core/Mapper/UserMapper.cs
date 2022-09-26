using OngProject.Core.Models.DTOs;
using OngProject.Entities;

namespace OngProject.Core.Mapper
{
    public class UserMapper
    {

        public static UserGetDTO ToUserDTO (User user)
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

    }
}
