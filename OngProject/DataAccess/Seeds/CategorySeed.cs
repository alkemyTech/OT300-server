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
                    Description = "Animals and pets"
                },
                new Category()
                {
                    Id = 2,
                    Name = "Art and culture",
                    Description = "Art and culture"
                },
                new Category()
                {
                    Id = 3,
                    Name = "Education",
                    Description = "Education"
                },
                new Category()
                {
                    Id = 4,
                    Name = "International aid",
                    Description = "International aid"
                },
                new Category()
                {
                    Id = 5,
                    Name = "Disability",
                    Description = "Disability"
                },
                new Category()
                {
                    Id = 6,
                    Name = "6",
                    Description = "6"
                },
                new Category()
                {
                    Id = 7,
                    Name = "7",
                    Description = "7"
                },
                new Category()
                {
                    Id = 8,
                    Name = "8",
                    Description = "8"
                },
                new Category()
                {
                    Id = 9,
                    Name = "9",
                    Description = "9"
                },
                new Category()
                {
                    Id = 10,
                    Name = "10",
                    Description = "10"
                },
                new Category()
                {
                    Id = 11,
                    Name = "11",
                    Description = "11"
                },
                new Category()
                {
                    Id = 12,
                    Name = "12",
                    Description = "12"
                },
                new Category()
                {
                    Id = 13,
                    Name = "13",
                    Description = "13"
                },
                new Category()
                {
                    Id = 14,
                    Name = "14",
                    Description = "14"
                },
                new Category()
                {
                    Id = 15,
                    Name = "15",
                    Description = "15"
                },
                new Category()
                {
                    Id = 16,
                    Name = "16",
                    Description = "16"
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
