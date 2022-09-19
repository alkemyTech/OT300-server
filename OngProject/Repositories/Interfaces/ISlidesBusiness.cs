using System.Threading.Tasks;

namespace OngProject.Repositories.Interfaces
{
    public interface ISlidesBusiness
    {
        public Task<bool> RemoveSlide(int id);
    }
}
