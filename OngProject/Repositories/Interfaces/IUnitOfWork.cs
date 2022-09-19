using OngProject.Entities;

namespace OngProject.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IRoleRepository RoleRepository { get; }
        IRepositoryBase<User> UserRepository { get; }
        void SaveChanges();
    }
}