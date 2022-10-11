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
using System;

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

        public async Task<CategoryFullDTO> Add(CategoryPostDTO newDTO)
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
                if (newDTO.File is not null)
                {
                    var path = newDTO.File.Length == 0 ?
                            "" :
                            await _imageStorageHerlper.UploadImageAsync(newDTO.File, $"category-{entity.Name}.jpg");

                    entity.Image = path;
                }

                var newCategory = await _unitOfWork.CategoryRepository.Add(entity);
                await _unitOfWork.SaveChangesAsync();


                return newCategory.ToFullDTO();
            }
        }

        public async Task Delete(int id)
        {
                await _unitOfWork.CategoryRepository.Delete(id);
                await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DoesExist(int id)
        {
            return await _unitOfWork.CategoryRepository.EntityExist(id);
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
            return (entity.IsDeleted) ? new CategoryFullDTO() : entity.ToFullDTO();
        }
        public async Task<Category> GetEntityById(int id)
        {
            var entity = await _unitOfWork.CategoryRepository.GetById(id);
            return  entity;
        }
        public async Task<CategoryPostDTO> UpdateCategory(int id, CategoryPostDTO categoryPostDTO)
        {
            var category = await _unitOfWork.CategoryRepository.GetById(id);

            if(category != null)
            {
                category.Name = categoryPostDTO.Name;
                category.Description = categoryPostDTO.Description ?? category.Description;
                
                if(categoryPostDTO.File is null || categoryPostDTO.File.Length == 0)
                {
                    category.Image = category.Image;
                }
                else
                {
                    var filename = "Category-" + categoryPostDTO.Name + "-" + Guid.NewGuid().ToString() + ".jpg";
                    category.Image = await _imageStorageHerlper.UploadImageAsync(categoryPostDTO.File, filename);
                    
                }

                await _unitOfWork.CategoryRepository.Update(category);
                await _unitOfWork.SaveChangesAsync();

                
            }
            return categoryPostDTO;
        }
    }
}