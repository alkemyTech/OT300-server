using OngProject.DataAccess;
using OngProject.Services.Interfaces;

namespace OngProject.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OngDbContext _dbContext;

        public UnitOfWork(OngDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void SaveChanges()
        {
            _dbContext.SaveChangesAsync();
        }
    }
}