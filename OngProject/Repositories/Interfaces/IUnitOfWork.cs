
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System.Threading.Tasks;


namespace OngProject.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IRepositoryBase<Member> MembersRepository { get; }
        IRepositoryBase<Role> RolesRepository { get; }
        IRepositoryBase<Organization> OrganizationRepository { get; }
        IRepositoryBase<Categories> CategoriesRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();
}
}