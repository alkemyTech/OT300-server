using OngProject.Core.Interfaces;
using OngProject.Repositories.Interfaces;
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
    }
}
