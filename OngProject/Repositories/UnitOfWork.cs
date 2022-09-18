using OngProject.DataAccess;
using OngProject.Repositories.Interfaces;

namespace OngProject.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OngDbContext _dbContext;
        private readonly IRoleRepository _roleRepository;

        public UnitOfWork(OngDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRoleRepository RoleRepository =>  _roleRepository ?? new RoleRepository(dbContext: _dbContext);

        public void SaveChanges()
        {
            _dbContext.SaveChangesAsync();
        }
    }
}