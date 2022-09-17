using OngProject.Repositories.Interfaces;

namespace OngProject.Services.Interfaces
{
    public interface IUnitOfWork
    {
        IRoleRepository RoleRepository { get; }
        void SaveChanges();
    }
}