using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Collections.Generic;

namespace OngProject
{
    public interface IUserBusiness 
    {
        public IEnumerable<UserGetDTO> GetAllUsers();
    }
}
