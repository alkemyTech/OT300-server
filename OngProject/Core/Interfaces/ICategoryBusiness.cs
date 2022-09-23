using OngProject.Core.Models.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace OngProject.Core.Interfaces
{
    public interface ICategoryBusiness
    {
        IEnumerable<CategoryDTO> GetAllCatNames();
    }
}
