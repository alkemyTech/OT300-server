using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Data.Common;

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
                testimonial.Name = testimonialUpdateDTO.Name ?? testimonial.Name;
                testimonial.Content = testimonialUpdateDTO.Content ?? testimonial.Content;
            }


            return testimonial;
        }

        public static TestimonialListDTO testimonialToDTO(this Testimonial testimonial)
        {
            TestimonialListDTO testimonialListDTO = new TestimonialListDTO()
            {
                Id = testimonial.Id,
                Name=testimonial.Name,
                Image=testimonial.Image,
                Content=testimonial.Content
            };
            return testimonialListDTO;
        }
    }
}
