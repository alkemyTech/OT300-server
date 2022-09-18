using OngProject.Core.Interfaces;
using OngProject.DataAccess;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class SlidesBusiness : ISlidesBusiness
    {
        private readonly OngDbContext _context;

        public SlidesBusiness(OngDbContext context)
        {
            _context = context;
        }
        public Task<bool> RemoveSlide(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
