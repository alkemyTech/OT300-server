using OngProject.Core.Models.DTOs;
using OngProject.Entities;

namespace OngProject.Core.Mapper
{
    public static class TestimonialMapper
    {
        public static Testimonial DtoToTestimonial(this TestimonialDTO testimonialDTO)
        {
            Testimonial testimonial = new Testimonial()
            {
                Name = testimonialDTO.Name,
                Content = testimonialDTO.Content
            };


            return testimonial;
        }
    }
}
