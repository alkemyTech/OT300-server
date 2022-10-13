using OngProject.Entities;
using System;

namespace OngProject.DataAccess.Seeds
{
    public class SlideSeed
    {
        public static Slide[] GetData()
        {
            var slides = new[]
            {
                new Slide{},
                new Slide{},
                new Slide{},
                new Slide{},
                new Slide{},
                new Slide{},
                new Slide{},
                new Slide{},
                new Slide{},
                new Slide{},
                new Slide{},
                new Slide{},
                new Slide{},
                new Slide{},
                new Slide{}
            };
            var iter = 1;
            foreach (var s in slides)
            {
                s.ImageUrl = $"/image/Slides/{iter}.jpg";
                s.OrganizationId = 1;
                s.Text = $"Lorem Ipsum{iter}";
                s.Id = iter;
                s.Order = iter;
                s.CreatedAt = DateTime.Now;
                s.IsDeleted = false;
                s.LastEditedAt = DateTime.Now;
                iter++;
            }

            return slides;

        }
    }
}
