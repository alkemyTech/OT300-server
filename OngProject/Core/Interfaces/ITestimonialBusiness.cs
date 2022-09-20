using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ITestimonialBusiness : IRepositoryBase<Testimonial>
    {
        IEnumerable<Testimonial> GetAll();
        Task<Testimonial> GetById(int id);
        Task Add(Testimonial testimonial);
        Task<bool> Update(Testimonial testimonial);
        Task<bool> Delete(int id);
    }
}
