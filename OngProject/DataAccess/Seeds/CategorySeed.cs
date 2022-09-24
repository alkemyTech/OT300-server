using OngProject.Entities;
using System;

namespace OngProject.DataAccess.Seeds
{
    public static class CategorySeed
    {
        public static Category[] GetData()
        {
            var category = new[]
            {
                new Category()
                {
                    Id = 1,
                    Name = "Animals and pets",
                    Image = "",
                    Description = "Animals and pets"
                },
                new Category()
                {
                    Id = 2,
                    Name = "Art and culture",
                    Image = "",
                    Description = "Art and culture"
                },
                new Category()
                {
                    Id = 3,
                    Name = "Education",
                    Image = "",
                    Description = "Education"
                },
                new Category()
                {
                    Id = 4,
                    Name = "International aid",
                    Image = "",
                    Description = "International aid"
                },
                new Category()
                {
                    Id = 5,
                    Name = "Disability",
                    Image = "",
                    Description = "Disability"
                }
            };

            var Number = 1;
            foreach (var n in category)
            {
                n.Image = $"/OT300/ong/category/img{Number}.jpg";
                n.CreatedAt = DateTime.Now;
                n.IsDeleted = false;
                n.LastEditedAt = DateTime.Now;
                Number++;
            }

            return category;
        }
    }
}
