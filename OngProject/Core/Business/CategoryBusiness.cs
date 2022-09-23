using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;
using System.Collections.Generic;

namespace OngProject.Core.Business
{
    public class CategoryBusiness : ICategoryBusiness
    {

        private readonly IUnitOfWork _unitOfWork;

        public CategoryBusiness ( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
    }
}
