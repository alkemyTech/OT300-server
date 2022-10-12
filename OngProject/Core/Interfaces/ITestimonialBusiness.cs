using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ITestimonialBusiness
    {
        PagedList<TestimonialListDTO> GetAllPaged(int page);
        Task<Testimonial> GetById(int id);
        Task<TestimonialDTO> Add(TestimonialDTO testimonial);
        Task<TestimonialUpdateDTO> Update(int id, TestimonialUpdateDTO testimonialUpdateDTO);
        Task<bool> Delete(int id);
        Task<bool> DoesExist(int id);

    }
}