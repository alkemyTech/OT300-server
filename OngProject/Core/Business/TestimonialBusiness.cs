using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
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

        public async Task<TestimonialDTO> Add(TestimonialDTO testimonialDTO)
        {

            Testimonial testimonial = new Testimonial();

            //set the file name for the path
            var fileName = "Testimonial-" + testimonialDTO.Name + ".jpg";

            //Map the DTO so when the Entity is added have all the information for the Database
            testimonial = testimonialDTO.DtoToTestimonial();

            //first Check if there is an image and if it had information, and if it's not return an empty string, otherwise upload the image and return it's path
            if (testimonialDTO.Image.Length == 0 || testimonialDTO.Image is null)
            {
                testimonial.Image = "";
            }
            else
            {
                testimonial.Image = await _imageStorageHerlper.UploadImageAsync(testimonialDTO.Image, fileName);
            }


            await _unitOfWork.TestimonialRepository.Add(testimonial);
            await _unitOfWork.SaveChangesAsync();

            return testimonialDTO;

        }

        public async Task<bool> Delete(int id)
        {
            var testimonials = await _unitOfWork.TestimonialRepository.GetById(id);

            if (testimonials == null)
            {
                if (testimonials == null)
                {
                    throw new  Exception("The Testimony does not exist");
                }
              
            }

             await _unitOfWork.TestimonialRepository.Delete(id);

            return true;
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