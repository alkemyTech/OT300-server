using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static Amazon.S3.Util.S3EventNotification;
using static System.Net.Mime.MediaTypeNames;

namespace OngProject.Core.Business
{
    public class NewsBusiness : INewsBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private IImageStorageHerlper _imageStorageHerlper;

        public NewsBusiness(IUnitOfWork unitOfWork, IImageStorageHerlper imageStorageHerlper)
        {
            _unitOfWork = unitOfWork;
            _imageStorageHerlper = imageStorageHerlper;
        }

        public PagedList<NewsDTO> GetAllPage(int page = 1)
        {
            if (page < 1) throw new ArgumentException("Argument must be greater than 0", "page");
            var news = _unitOfWork.NewsRepository.GetAll(page);
            var dtos = news.ToDTO();
            var pDTOs = new PagedList<NewsDTO>(dtos, news.TotalCount, page, 10);
            return pDTOs;
        }

        public async Task<News> GetById(int id)
        {
            return await _unitOfWork.NewsRepository.GetById(id);
        }

        public async Task<NewsFullDTO> Add(NewsPostDTO news)
        {
            var categoryExists = await _unitOfWork.CategoryRepository.GetById(news.IdCategory);
            if (categoryExists == null) return new NewsFullDTO();

            var entity = news.ToEntity();
            var path = news.ImageFile.Length == 0 ? "" :
                await _imageStorageHerlper.UploadImageAsync(news.ImageFile, $"news-{entity.Name}.jpg");

            entity.Image = path;
            await _unitOfWork.NewsRepository.Add(entity);
            _unitOfWork.SaveChanges();

            return entity.ToFullDTO();
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

        public async Task Delete(int id)
        {
            await _unitOfWork.NewsRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DoesExist(int id)
        {
            return await _unitOfWork.NewsRepository.EntityExist(id);
        }
    }
}