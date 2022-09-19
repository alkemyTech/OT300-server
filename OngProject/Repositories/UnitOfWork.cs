using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System.Threading.Tasks;


namespace OngProject.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OngDbContext _dbContext;

        private readonly IRepositoryBase<Members> _membersRepository;


        public UnitOfWork(OngDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IRepositoryBase<Members> MembersRepository => _membersRepository ?? new RepositoryBase<Members>(_dbContext);

        

        public void SaveChanges()
        {
            _dbContext.SaveChangesAsync();
        }


        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

    }
}