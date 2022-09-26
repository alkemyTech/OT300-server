using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System.Threading.Tasks;


namespace OngProject.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OngDbContext _dbContext;

        private readonly IRepositoryBase<Activity> _activityRepository;
        private readonly IRepositoryBase<Category> _categoryRepository;
        private readonly IRepositoryBase<Member> _memberRepository;
        private readonly IRepositoryBase<News> _newsRepository;
        private readonly IRepositoryBase<Organization> _organizationRepository;
        private readonly IRepositoryBase<Role> _roleRepository;
        private readonly IRepositoryBase<Slide> _slideRepository;
        private readonly IRepositoryBase<Testimonial> _testimonialRepository;
        private readonly IRepositoryBase<User> _userRepository;
        private IRepositoryBase<Contact> _contactRepository;
        private IRepositoryBase<Comment> _commentRepository;


        public UnitOfWork(OngDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepositoryBase<Activity> ActivityRepository => _activityRepository ?? new RepositoryBase<Activity>(_dbContext);
        public IRepositoryBase<Category> CategoryRepository => _categoryRepository ?? new RepositoryBase<Category>(_dbContext);
        public IRepositoryBase<Member> MembersRepository => _memberRepository ?? new RepositoryBase<Member>(_dbContext);
        public IRepositoryBase<News> NewsRepository => _newsRepository ?? new RepositoryBase<News>(_dbContext);
        public IRepositoryBase<Organization> OrganizationRepository => _organizationRepository ?? new RepositoryBase<Organization>(_dbContext);
        public IRepositoryBase<Role> RoleRepository => _roleRepository ?? new RepositoryBase<Role>(_dbContext);
        public IRepositoryBase<Slide> SlideRepository => _slideRepository ?? new RepositoryBase<Slide>(_dbContext);
        public IRepositoryBase<Testimonial> TestimonialRepository => _testimonialRepository ?? new RepositoryBase<Testimonial>(_dbContext);
        public IRepositoryBase<User> UserRepository => _userRepository ?? new RepositoryBase<User>(_dbContext);
        public IRepositoryBase<Contact> ContactRepository => _contactRepository ?? new RepositoryBase<Contact>(_dbContext);
        public IRepositoryBase<Comment> CommentRepository => _commentRepository ?? new RepositoryBase<Comment>(_dbContext);





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