using System.Collections.Generic;
using System.Threading.Tasks;
using OngProject.Entities;

namespace OngProject.Core.Interfaces
{
    public interface IRoleBusiness
    {
        Task<Role> Create(Role request);
        IEnumerable<Role> GetAll();
        Task<Role> GetById(int id);
        Task<Role> Update(Role role);
        Task Delete(Role role);
    }
}