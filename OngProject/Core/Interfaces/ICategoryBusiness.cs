using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ICategoryBusiness
    {
        IEnumerable<CategoryDTO> GetAllCatNames();
        Task<CategoryFullDTO> Add(CategoryPostDTO categoryFullDTO);
        Task<CategoryFullDTO> GetById(int id);
        Task<bool> Delete(int id);

    }
}
