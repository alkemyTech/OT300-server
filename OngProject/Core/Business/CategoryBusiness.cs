using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class CategoryBusiness : ICategoryBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageStorageHerlper _imageStorageHerlper;

        public CategoryBusiness(IUnitOfWork unitOfWork, IImageStorageHerlper imageStorageHerlper)
        {
            _unitOfWork = unitOfWork;
            _imageStorageHerlper = imageStorageHerlper;
        }

        public async Task<CategoryFullDTO> Add(CategoryPostDTO newDTO, Stream image)
        {
            var categories = _unitOfWork.CategoryRepository.GetAll();
            var exists = categories.Any(x => x.Name == newDTO.Name);

            if (exists)
            {
                return new CategoryFullDTO();
            }
            else
            {
                var entity = newDTO.ToEntity();

                //TODO: Check if stream is an image
                //TODO: Check image format for extension.

                var path = image.Length == 0 ? "" :
                  await _imageStorageHerlper.UploadImageAsync(image, $"category-{entity.Name}.jpg");
                entity.Image = path;

                var newCategory = await _unitOfWork.CategoryRepository.Add(entity);
                await _unitOfWork.SaveChangesAsync();


                return newCategory.ToFullDTO();
            }
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _unitOfWork.CategoryRepository.GetById(id);
            if (entity == null || entity.IsDeleted) return false;

            try
            {
                await _unitOfWork.CategoryRepository.Delete(id);
                return true;
            }
            catch (System.Exception)
            {
                //Log()
                throw;
            }
        }

        public IEnumerable<CategoryDTO> GetAllCatNames()
        {
            var categories = _unitOfWork.CategoryRepository.GetAll();
            var catDTO = new List<CategoryDTO>();

            foreach (var category in categories)
            {
                catDTO.Add(CategoryMapper.CategoryToCategoryDTO(category));
            }

            return catDTO;
        }

        public async Task<CategoryFullDTO> GetById(int id)
        {
            var entity = await _unitOfWork.CategoryRepository.GetById(id);
            return  (entity.IsDeleted)?new CategoryFullDTO(): entity.ToFullDTO();
        }
    }
}
