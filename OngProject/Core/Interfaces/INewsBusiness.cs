using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface INewsBusiness
    {
        PagedList<NewsDTO> GetAllPage(int page);
        Task<News> GetById(int id);
        Task<NewsFullDTO> Add(NewsPostDTO dto);
        Task<NewsPutDTO> Update(int id, NewsPutDTO newsDto);
        Task Delete(int id);
        Task<bool> DoesExist(int id);
    }
}