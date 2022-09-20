using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class NewsBusiness : INewsBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly NewsBusiness _newsBusiness;

        public NewsBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<News> Add(News news)
        {
            await _unitOfWork.NewsRepository.Add(news);
            _unitOfWork.SaveChanges();
            return news;
        }

        public async Task<bool> Delete(int id)
        {
            var newsToDelete = await _unitOfWork.NewsRepository.GetById(id);
            if (newsToDelete != null)
            {
                newsToDelete.IsDeleted = true;
                _unitOfWork.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<NewsDTO> GetAll()
        {
            return (IEnumerable<NewsDTO>)_unitOfWork.NewsRepository.GetAll();
        }

        public async Task<News> GetById(int id)
        {
            return await _unitOfWork.NewsRepository.GetById(id);
        }

        public async Task<News> Update(News news)
        {
            var newsToUpdate = await _unitOfWork.NewsRepository.GetById(news.Id);
            if (newsToUpdate != null)
            {
                await _unitOfWork.NewsRepository.Update(news);
                _unitOfWork.SaveChanges();
                return news;
            }
            else
            {
                return null;
            }
        }
    }
}
