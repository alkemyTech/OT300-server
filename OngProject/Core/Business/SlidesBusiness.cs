using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class SlidesBusiness : ISlideBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageStorageHerlper _imageStorageHelper;

        public SlidesBusiness(IUnitOfWork unitOfWork, IImageStorageHerlper imageStorageHelper)
        {
            _unitOfWork = unitOfWork;
            _imageStorageHelper = imageStorageHelper;
        }


        public async Task RemoveSlide(int id)
        {
            await _unitOfWork.SlideRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();

        }

        /// <summary>
        /// Get the  slides from the organization
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SlideDTO> GetAll()
        {
            var slides = _unitOfWork.SlideRepository.GetAll();
            var dtos = SlideMapper.ToDTO(slides);
            return dtos;
        }

        public IEnumerable<SlidePublicDTO> GetAllSlides()
        {
            var slides = _unitOfWork.SlideRepository.GetAll().OrderBy(or => or.Order);
            var slidesDTO = new List<SlidePublicDTO>();
            foreach (var slide in slides)
            {
                slidesDTO.Add(SlideMapper.ToPublicDTO(slide));
            }

            return slidesDTO;
        }
        public async Task<Slide> GetById(int id)
        {
            return await _unitOfWork.SlideRepository.GetById(id);
        }

        public async Task<SlideResponseDTO> Create(SlideCreateDTO slide)
        {

            var stream = slide.ImageStream.OpenReadStream();
            var fileName = slide.ImageStream.FileName.ToLower();

            var imageUrl = await _imageStorageHelper.UploadImageAsync(stream, fileName);

            var slideToCreate = SlideMapper.ToSlideEntity(slide);
            slideToCreate.ImageUrl = imageUrl;

            var slideCreated = await _unitOfWork.SlideRepository.Add(slideToCreate);

            await _unitOfWork.SaveChangesAsync();

            return slideCreated.ToSlideResponseDTO();
        }

        public async Task<bool> Update(int id, SlideCreateDTO slide)
        {
            var actualSlide = await _unitOfWork.SlideRepository.GetById(id);
            //addError(
            if (slide.OrganizationId != actualSlide.OrganizationId) return false;
            if (actualSlide is not null)
            {
                var stream = slide.ImageStream?.OpenReadStream();
                if (stream is not null || stream.Length != 0)
                {
                    //TODO Establecer reglas de Naming de las imagenes
                    // var imageUrl = await _imageStorageHelper.UploadImageAsync(stream, $"slide-{id}.jpg");
                    try
                    {
                        actualSlide.ImageUrl = await _imageStorageHelper.UploadImageAsync(stream, slide.ImageStream.FileName);
                    }
                    catch (Exception ex)
                    {
                        //TODO BusinessResponse<T> y BusinessPagedResponse<T> if error happens
                        //log
                        actualSlide.ImageUrl = $"{ex.Message}-upload-error.jpg";
                    }
                }
                //mapping
                actualSlide.SetNewValues(slide);

                try
                {
                    var updated = await _unitOfWork.SlideRepository.Update(actualSlide);
                    await _unitOfWork.SaveChangesAsync();
                    return updated is not null;
                }
                catch (Exception)
                {
                    //log
                    // addError(
                    //status = failed
                    throw;
                };

            }
            // TODO refactor para respuestas unificadas
            //addError(
            //status = failed
            //return BusinessResponse
            return false;
        }

        public async Task<bool> DoesExist(int id)
        {
            return await _unitOfWork.SlideRepository.EntityExist(id);
        }
    }
}