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
        Task<MemberUpdateDTO> Update(int id, MemberUpdateDTO memberUpdateDTO);
        Task Delete(int id);
        Task<bool> DoesExist(int id);
    }
}