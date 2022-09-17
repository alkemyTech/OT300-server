using System.Collections.Generic;
using OngProject.Entities;

namespace OngProject.Services.Interfaces
{
    public interface IRoleService
    {
        Role Create(Role request);
        IEnumerable<Role> GetAll();
        Role GetById(int id);
        Role Update(Role role);
        void Delete(Role role);
    }
}