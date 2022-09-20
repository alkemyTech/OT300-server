using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using OngProject.Core.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class TestimonialBusiness : ITestimonialBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

        public TestimonialBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task Add(Testimonial testimonial)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Testimonial> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Testimonial> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Testimonial testimonial)
        {
            throw new NotImplementedException();
        }

        Task<Testimonial> IRepositoryBase<Testimonial>.Add(Testimonial entity)
        {
            throw new NotImplementedException();
        }

        Task IRepositoryBase<Testimonial>.Delete(int id)
        {
            throw new NotImplementedException();
        }

        Task<Testimonial> IRepositoryBase<Testimonial>.Update(Testimonial entity)
        {
            throw new NotImplementedException();
        }
    }
}