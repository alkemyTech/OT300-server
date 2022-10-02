using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IMemberBusiness
    {
        IEnumerable<MembersDTO> GetAll();
        Task<Member> GetById(int id);
        Task<MembersDTO> Add(MembersDTO members);
        Task<bool> Update(Member members);
        Task<bool> Delete(int id);
    }
}
