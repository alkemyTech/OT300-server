using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ISlideBusiness
    {
        public Task<bool> RemoveSlide(int id);
        public IEnumerable<SlideDTO> GetAll();
        public IEnumerable<SlidePublicDTO> GetAllSlides();
        Task<Slide> Create(SlideCreateDTO slide);
    }
}