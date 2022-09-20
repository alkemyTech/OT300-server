using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface INewsBusiness
    {
        IEnumerable<NewsDTO> GetAll();
        Task<News> GetById(int id);
        Task<News> Add(News news);
        Task<News> Update(News news);
        Task<bool> Delete(int id);
    }
}
