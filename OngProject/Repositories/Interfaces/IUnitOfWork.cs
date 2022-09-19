
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System.Threading.Tasks;


namespace OngProject.Repositories.Interfaces
{
    public interface IUnitOfWork
    {

        IRepositoryBase<Members> MembersRepository { get; }
        IRepositoryBase<Categories> CategoriesRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();
}
}