using Microsoft.EntityFrameworkCore;
using OngProject.DataAccess.Seeds;
using OngProject.Entities;
using System.Reflection;

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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<Member>().HasData(MemberSeed.GetData());
            modelBuilder.Entity<Testimonial>().HasData(TestimonialSeed.GetData());
            modelBuilder.Entity<News>().HasData(NewsSeed.GetData());
        }
    }
}