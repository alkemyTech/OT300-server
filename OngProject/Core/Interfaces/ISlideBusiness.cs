using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ISlideBusiness
    {
        public Task RemoveSlide(int id);
        public IEnumerable<SlideDTO> GetAll();
        public IEnumerable<SlidePublicDTO> GetAllSlides();
        Task<SlideResponseDTO> Create(SlideCreateDTO slide);
        Task<Slide> GetById(int id);
        Task<bool> DoesExist(int id);
    }
}