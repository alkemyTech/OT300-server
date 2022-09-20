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
        public User Insert(Testimonial request)
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

        public Task<Testimonial> Update(Testimonial user)
        {
            throw new NotImplementedException();
        }

        public void Delete(Testimonial user)
        {
            throw new NotImplementedException();
        }

        public Task<Testimonial> Add(Testimonial entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }


    }
}