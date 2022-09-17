using OngProject.DataAccess;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;
using OngProject.Services.Interfaces;

namespace OngProject.Services
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