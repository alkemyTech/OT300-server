namespace OngProject.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IRoleRepository RoleRepository { get; }
        void SaveChanges();
    }
}