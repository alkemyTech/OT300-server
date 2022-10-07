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

        public static Testimonial UpdateDtoToTestimonial(this Testimonial testimonial, TestimonialUpdateDTO testimonialUpdateDTO)
        {
            //Testimonial testimonial = new Testimonial()
            {
                testimonial.Name = testimonialUpdateDTO.Name ?? string.Empty;
                testimonial.Content = testimonialUpdateDTO.Content ?? string.Empty;
            }


            return testimonial;
        }
    }
}
