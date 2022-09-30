using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface INewsBusiness
    {
        IEnumerable<NewsDTO> GetAll();
        Task<News> GetById(int id);
        Task<NewsFullDTO> Add(NewsPostDTO dto);
        Task<News> Update(News news);
        Task Delete(int id);
    }
}
