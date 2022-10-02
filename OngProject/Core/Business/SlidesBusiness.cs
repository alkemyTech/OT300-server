using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OngProject.Core.Helper;
using OngProject.Core.Mapper;

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
        public Task<bool> RemoveSlide(int id)
        {
            throw new System.NotImplementedException();
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

        public async Task<Slide> GetById(int id)
        {
           return await _unitOfWork.SlideRepository.GetById(id);
        }
    }
}