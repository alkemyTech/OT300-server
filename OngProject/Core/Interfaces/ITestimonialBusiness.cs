using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ITestimonialBusiness
    {
        IEnumerable<Testimonial> GetAll();
        Task<Testimonial> GetById(int id);
        Task<TestimonialDTO> Add(TestimonialDTO testimonial);
        Task<bool> Update(Testimonial testimonial);
        Task<bool> Delete(int id);
        Task<bool> DoesExist(int id);

    }
}