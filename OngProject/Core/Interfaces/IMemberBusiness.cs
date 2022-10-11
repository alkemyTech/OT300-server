using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IMemberBusiness
    {
        PagedList<MembersDTO> GetAll(PaginationParams paginationParams);
        Task<Member> GetById(int id);
        Task<MembersDTO> Add(MembersDTO members);
        Task<bool> Update(Member members);
        Task Delete(int id);
        Task<bool> DoesExist(int id);
    }
}