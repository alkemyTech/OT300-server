using OngProject.Core.Interfaces;
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

        public SlidesBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<bool> RemoveSlide(int id)
        {
            throw new System.NotImplementedException();
        }

        //public IEnumerable<Slide> GetAll()
        //{
        //    throw new NotImplementedException();

        //}

        /// <summary>
        /// Get the  slides from the organization
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SlideDTO> GetAll()
        {
            var slides = _unitOfWork.SlideRepository.GetAll();
            var dtos = new List<SlideDTO>();

            #region Mapper
            foreach (var slide in slides)
            {
                dtos.Add(new SlideDTO()
                {
                  //  Id = slide.Id,
                    ImageUrl = slide.ImageUrl,
                    Order = slide.Order,
                    Text = $"caption-{slide.Id}"
                });
            } 
            #endregion
            return dtos;
        }
    }
}
