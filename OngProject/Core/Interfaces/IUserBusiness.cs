using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace OngProject
{
    public interface IUserBusiness 
    {
        public IEnumerable<UserGetDTO> GetAllUsers();
        public Task<User> GetById(int id);
        public Task Delete(int id);
    }
}
