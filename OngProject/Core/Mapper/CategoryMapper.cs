using OngProject.Core.Models.DTOs;
using OngProject.Entities;

namespace OngProject.Core.Mapper
{
    public static class CategoryMapper
    {

        public static CategoryDTO CategoryToCategoryDTO(Category category)
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
    }
}
