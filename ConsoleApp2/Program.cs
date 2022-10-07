using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories;
using System;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
        private static OngDbContext _context;
        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
            .AddDbContext<OngDbContext>(o =>
                o.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ONG;Trusted_Connection=True;MultipleActiveResultSets=true"))
            .BuildServiceProvider();

            _context = serviceProvider.GetRequiredService<OngDbContext>();


            var uow = new UnitOfWork(_context);
            //Test PagedList
            //add more tan 10 categories
            //for (int i = 0; i < 30; i++)
            //{
            //    await uow.CategoryRepository.Add(new Category()
            //    {
            //        Description = $"category-{i}",
            //        Image= $"image-{i}",
            //        LastEditedAt = DateTime.Now,
            //        CreatedAt = DateTime.Now,
            //        Name = $"name-{i}"
            //    });
            //}

            //await uow.SaveChangesAsync();

            //get paged list
            var page1 = uow.CategoryRepository.GetAll(1);
            page1.ForEach(x => Console.WriteLine(x.Name));
            Console.WriteLine("------------------------------");
            var page2 = uow.CategoryRepository.GetAll(2);
            
          
            page2.ForEach(x => Console.WriteLine(x.Name));
        }


        static async void Test_OrganizationUpdate()
        {
            var orgs = await _context.Organizations.ToListAsync();
            var uow = new UnitOfWork(_context);

            // var newOrgToUpdate = new Organization() { Name = "2", Email="", PhoneNumber = 1,Img="",FacebookUrl="",InstagramUrl="",LinkedInUrl="",WelcomeText="" };

            // await uow.OrganizationRepository.Add(newOrgToUpdate);
            //  await uow.SaveChangesAsync();

            var newOrgToUpdate = new Organization() { Id = 2, Name = "2", Email = "", PhoneNumber = 1, Img = "", FacebookUrl = "", InstagramUrl = "", LinkedInUrl = "", WelcomeText = "" };
            newOrgToUpdate.CreatedAt = newOrgToUpdate.LastEditedAt = DateTime.Now;
            // newOrgToUpdate.Name = "asd";
            await uow.OrganizationRepository.Add(newOrgToUpdate);
            newOrgToUpdate.Name = "asd";
            var updated = await uow.OrganizationRepository.Update(newOrgToUpdate);

            await uow.SaveChangesAsync();
        }
    }
}
