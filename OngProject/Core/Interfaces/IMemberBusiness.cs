using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IMemberBusiness
    {
        IEnumerable<Members> GetAll();
        Task<Members> GetById(int id);
        Task Add(Members members);
        Task<bool> Update(Members members);
        Task<bool> Delete(int id);
    }
}
