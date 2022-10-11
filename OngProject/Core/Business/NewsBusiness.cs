using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
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

        public async Task Delete(int id)
        {
            await _unitOfWork.NewsRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DoesExist(int id)
        {
            return await _unitOfWork.NewsRepository.EntityExist(id);
        }


        public IEnumerable<NewsDTO> GetAll()
        {
            return (IEnumerable<NewsDTO>)_unitOfWork.NewsRepository.GetAll();
        }

        public async Task<News> GetById(int id)
        {
            return await _unitOfWork.NewsRepository.GetById(id);
        }

        public async Task<NewsPutDTO> Update(int id, NewsPutDTO newsDto)
        {
            var newsToUpdate = await _unitOfWork.NewsRepository.GetById(id);

            newsToUpdate.NewsUpdate(newsDto);

            if (newsDto.ImageFile is null || newsDto.ImageFile.Length == 0)
            {
                newsToUpdate.Image = newsToUpdate.Image;
            }
            else
            {
                var fileName = "News-" + newsDto.Name + "-" + Guid.NewGuid().ToString() + ".jpg";
                newsToUpdate.Image = await _imageStorageHerlper.UploadImageAsync(newsDto.ImageFile, fileName);
            }

            await _unitOfWork.NewsRepository.Update(newsToUpdate);
            _unitOfWork.SaveChanges();

            return newsDto;

            
        }
    }
}