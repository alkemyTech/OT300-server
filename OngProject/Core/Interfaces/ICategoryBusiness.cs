using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ICategoryBusiness
    {
        IEnumerable<CategoryDTO> GetAllCatNames();
        PagedList<CategoryDTO> GetAll(int page);
        Task<CategoryFullDTO> Add(CategoryPostDTO categoryFullDTO);
        Task<CategoryFullDTO> GetById(int id);
        Task<Category> GetEntityById(int id);
        Task Delete(int id);
        Task<bool> DoesExist(int id);

    }
}