
using Microsoft.EntityFrameworkCore;
using OngProject.Entities;
using System.Reflection;

namespace OngProject.DataAccess
{
    public class OngDbContext : DbContext
    {
        public OngDbContext(DbContextOptions<OngDbContext> options): base(options)
        {

        }

        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Members> Members { get; set; }
        public virtual DbSet<Organization> Organization { get; set; }
        public virtual DbSet<Slides> Slide { get; set; }
        public virtual DbSet<Categories> Categorie { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}