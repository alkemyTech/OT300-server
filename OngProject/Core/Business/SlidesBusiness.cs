using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OngProject.Core.Mapper;

namespace OngProject.Core.Business
{
    public class SlidesBusiness : ISlideBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

        public SlidesBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        public IEnumerable<Slide> GetAllSlides()
        {
            return _unitOfWork.SlideRepository.GetAll().OrderBy(or => or.Order);
        }
    }
}
