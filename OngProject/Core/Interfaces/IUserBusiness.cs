using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject
{
    public interface IUserBusiness 
    {
        public IEnumerable<UserGetDTO> GetAllUsers();
        Task<UserGetDTO> GetById(int id);
    }
}
