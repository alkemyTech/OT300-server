using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using OngProject.DataAccess;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace OngProject.Tests
{
    [TestClass]
    public class Init
    {
        static TestServer _testServer;
        public static HttpClient Client;
        public static OngDbContext DbContext;
        public static string TokenAdmin;
        public static string TokenUser;

        [AssemblyInitialize]
        public static  async Task AssemblyInit(TestContext ctx)
        {
            var builder = new WebHostBuilder()
           .ConfigureAppConfiguration(configurationBuilder =>
           {
               configurationBuilder.AddJsonFile("appsettings.development.json");
           })
           .UseStartup<Startup>()
           .ConfigureTestServices(services =>
           {
               services.RemoveAll(typeof(DbContextOptions<OngDbContext>));
               services.AddDbContext<OngDbContext>(opt =>
               {
                   opt.UseInMemoryDatabase("ONGTest");
               });
           });

            _testServer = new TestServer(builder);
            Client = _testServer.CreateClient();

            DbContext = _testServer.Services.GetService<OngDbContext>();
            DbContext.Roles.AddRange(DataAccess.Seeds.RoleSeed.GetData());
            DbContext.Users.AddRange(DataAccess.Seeds.UserSeed.GetData());
            DbContext.SaveChanges();

            //Tokens
            var controller = "auth";
            //var logins
            var credentialsAdmin = new { email = "MartaJuarez@gmail.com", password = "123456" };
            var usersAdmin = new { email = "CarlosJuarez@gmail.com", password = "123456" };

            var jsonAdmin = JsonConvert.SerializeObject(credentialsAdmin);
            var jsonUser = JsonConvert.SerializeObject(usersAdmin);

            //as admin
            var response =
                await Client
                .PostAsync($"api/{controller}/login",
                new StringContent(jsonAdmin, Encoding.UTF8, "application/json")
                );

            TokenAdmin = await response.Content.ReadAsStringAsync();
            //as user
            response = await Client
                .PostAsync($"api/{controller}/login",
                new StringContent(jsonUser, Encoding.UTF8, "application/json"));

            TokenUser = await response.Content.ReadAsStringAsync();
        }

        [AssemblyCleanup]
        public static void Clean()
        {
            _testServer.Dispose();
            Client.Dispose();
            DbContext.Dispose();

        }
    }
}