using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Threading.Tasks;


namespace OngProject.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IRepositoryBase<Activity> ActivityRepository { get; }
        IRepositoryBase<Category> CategoryRepository { get; }
        IRepositoryBase<Member> MembersRepository { get; }
        IRepositoryBase<News> NewsRepository { get; }
        IRepositoryBase<Organization> OrganizationRepository { get; }
        IRepositoryBase<Role> RoleRepository { get; }
        IRepositoryBase<Slide> SlideRepository { get; }
        IRepositoryBase<Testimonial> TestimonialRepository { get; }
        IRepositoryBase<User> UserRepository { get; }
        IRepositoryBase<Contact> ContactRepository { get; }
        IRepositoryBase<Comment> CommentRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}