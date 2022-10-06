﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
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
            var fileName = "Testimonial-" + testimonialDTO.Name + Guid.NewGuid().ToString() + ".jpg";

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
                    throw new  Exception("The Testimony does not exist");
            }

             await _unitOfWork.TestimonialRepository.Delete(id);

            return true;
        }

        public Task<bool> DoesExist(int id)
        {
            return _unitOfWork.TestimonialRepository.EntityExist(id);
        }

        public IEnumerable<Testimonial> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Testimonial> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<TestimonialDTO> Update(int id, TestimonialDTO testimonialDto)
        {
            var testimonialToUpdate = await _unitOfWork.TestimonialRepository.GetById(id);

            //agregar GUID
            var fileName = "Testimonial-" + testimonialDto.Name + Guid.NewGuid().ToString() + ".jpg"; ;

            testimonialToUpdate.Image = await _imageStorageHerlper.UploadImageAsync(testimonialDto.Image, fileName);

            testimonialToUpdate = TestimonialMapper.DtoToTestimonial(testimonialDto);

            var updated = await _unitOfWork.TestimonialRepository.Update(testimonialToUpdate);
            await _unitOfWork.SaveChangesAsync();
            
            return testimonialDto;

        }


    }
}