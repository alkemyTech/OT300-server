using OngProject.Entities;
using System.Collections.Generic;

namespace OngProject
{
    public interface IUserBusiness 
    {
        public IEnumerable<User> GetAllUsers();
    }
}
