using OngProject.Entities;

namespace OngProject.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IRoleRepository RoleRepository { get; }
        IRoleRepository SlidesRepository { get; }

        IRoleRepository CategoriesRepository { get; }
        void SaveChanges();
    }
}