using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ISlideBusiness
    {
        public Task<bool> RemoveSlide(int id);
    }
}
