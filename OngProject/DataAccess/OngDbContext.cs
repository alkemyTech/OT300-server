using Microsoft.EntityFrameworkCore;
using OngProject.DataAccess.Seeds;
using OngProject.Entities;
using System.Reflection;
using System.Runtime.InteropServices;

namespace OngProject.DataAccess
{
    public class OngDbContext : DbContext
    {
        public OngDbContext(DbContextOptions<OngDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Slide> Slides { get; set; }
        public virtual DbSet<Testimonial> Testimonials { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Todo: Is this line of code really needed?
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Member>().HasData(MemberSeed.GetData());
            modelBuilder.Entity<Testimonial>().HasData(TestimonialSeed.GetData());
            modelBuilder.Entity<News>().HasData(NewsSeed.GetData());
            modelBuilder.Entity<Category>().HasData(CategorySeed.GetData());
            modelBuilder.Entity<Slide>().HasData(SlideSeed.GetData());
            modelBuilder.Entity<Role>().HasData(RoleSeed.GetData());
            modelBuilder.Entity<User>().HasData(UserSeed.GetData());                     
            modelBuilder.Entity<Organization>().HasData(OrganizationSeed.GetData());
            modelBuilder.Entity<Activity>().HasData(ActivitiesSeed.GetData());
            modelBuilder.Entity<Comment>().HasData(CommentSeed.GetData());
            modelBuilder.Entity<Contact>().HasData(ContactSeed.GetData());
        }
    }
}