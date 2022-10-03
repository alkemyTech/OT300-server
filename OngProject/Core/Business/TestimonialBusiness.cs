using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class TestimonialBusiness : ITestimonialBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageStorageHerlper _imageStorageHerlper;

        public TestimonialBusiness(IUnitOfWork unitOfWork, IImageStorageHerlper imageStorageHerlper)
        {
            _unitOfWork = unitOfWork;
            _imageStorageHerlper = imageStorageHerlper;
        }

        public async Task<TestimonialDTO> Add(TestimonialDTO testimonial)
        {

            Testimonial testimonialEntity = new Testimonial();

            var fileName = "Testimonial-" + testimonial.Name + ".jpg";

            testimonialEntity = testimonial.DtoToTestimonial();

            if (testimonial.Image.Length == 0 || testimonial.Image is null)
            {
                testimonialEntity.Image = "";
            }
            else
            {
                testimonialEntity.Image = await _imageStorageHerlper.UploadImageAsync(testimonial.Image, fileName);
            }


            await _unitOfWork.TestimonialRepository.Add(testimonialEntity);
            await _unitOfWork.SaveChangesAsync();

            return testimonial;

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