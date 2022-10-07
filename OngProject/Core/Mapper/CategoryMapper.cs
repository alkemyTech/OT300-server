using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Collections.Generic;

namespace OngProject.Core.Mapper
{
    public static class CategoryMapper
    {

        public static CategoryDTO CategoryToCategoryDTO(this Category category)
        {
            CategoryDTO categoryDTO = new CategoryDTO()
            {
                Name = category.Name
            };
            return categoryDTO;
        }

        public static Category ToEntity(this CategoryDTO dto)
        {
            Category category = new Category()
            {
                Name = dto.Name,
            };

            //TODO Refactor this
            if (dto.GetType().Name.Contains("Post"))
                category.Description = (dto as CategoryPostDTO).Description;

            return category;
        }

        public static Category ToEntity(this CategoryFullDTO dto)
        {
            Category category = new Category()
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Image = dto.Image
            };
            return category;
        }

        /// <summary>
        /// Convert Category  Entity to DTO
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static CategoryFullDTO ToFullDTO(this Category entity)
        {
            CategoryFullDTO categorydto = new CategoryFullDTO()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Image = entity.Image
            };
            return categorydto;
        }

        public static List<CategoryDTO> ToDTO(this IList<Category> entities)
        {
            var listDTOs = new List<CategoryDTO>();
            foreach (var item in entities)
            {
                listDTOs.Add(item.CategoryToCategoryDTO());
                // yield return item.ToFullDTO();
            }
            return listDTOs;
        }
    }
}
