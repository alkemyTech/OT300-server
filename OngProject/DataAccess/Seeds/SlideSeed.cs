using OngProject.Entities;
using System;

namespace OngProject.DataAccess.Seeds
{
    public class SlideSeed
    {
        public static Slide[] GetData()
        {
            var slides = new[] {
                new Slide()
                {
                    Id = 1,
                    ImageUrl = "image1",
                    Order = 1,
                    OrganizationId = 1
                    ,Text = "text2"
                },
                new Slide()
                {
                    Id = 2,
                    ImageUrl = "image2",
                    Order = 2,
                    OrganizationId = 1
                    ,Text = "text2"
                },


            };

            foreach (var s in slides)
            {
                s.CreatedAt = DateTime.Now;
                s.IsDeleted = false;
                s.LastEditedAt = DateTime.Now;
            }

            return slides;

        }
    }
}
