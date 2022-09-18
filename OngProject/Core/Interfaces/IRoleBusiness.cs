using System.Collections.Generic;
using OngProject.Entities;

namespace OngProject.Core.Interfaces
{
    public interface IRoleBusiness
    {
        Role Create(Role request);
        IEnumerable<Role> GetAll();
        Role GetById(int id);
        Role Update(Role role);
        void Delete(Role role);
    }
}