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
        private readonly IRepositoryBase<Categories> _categoriesRepository;

        public UnitOfWork(OngDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IRepositoryBase<Members> MembersRepository => _membersRepository ?? new RepositoryBase<Members>(_dbContext);
        public IRepositoryBase<Categories> CategoriesRepository => _categoriesRepository ?? new RepositoryBase<Categories>(_dbContext);
        public IRepositoryBase<Slides> SlidesRepository => _slidesRepository ?? new RepositoryBase<Slides>(_dbContext);



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