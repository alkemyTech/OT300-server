using OngProject.Core.Models.DTOs;
using OngProject.Entities;

namespace OngProject.Core.Mapper
{
    public class CategoryMapper
    {

        public static CategoryDTO CategoryToCategoryDTO(Category category)
        {
            CategoryDTO categoryDTO = new CategoryDTO()
            {
                Name = category.Name
            };
            return categoryDTO;
        }
    }
}
