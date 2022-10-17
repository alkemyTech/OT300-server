using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using OngProject.Core.Interfaces;
using OngProject.DataAccess;
using OngProject.Services.Interfaces;
using OngProject.Tests.ServicesMocks;
using System.Net.Http;
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
        public static async Task AssemblyInit(TestContext ctx)
        {
            //Test Host
            var builder = new WebHostBuilder()
               .ConfigureAppConfiguration(configurationBuilder =>
               {
                   configurationBuilder.AddJsonFile("appsettings.development.json");
               })
               .UseStartup<Startup>()
               .ConfigureTestServices(services =>
               {
                   services.AddScoped<IImageStorageHerlper, MockS3>();
                   services.AddScoped<IEmailService, MockEmail>();
                   services.RemoveAll(typeof(DbContextOptions<OngDbContext>));
                   services.AddDbContext<OngDbContext>(opt =>
                   {
                       opt.UseInMemoryDatabase("ONGTest");
                   });
               });

            _testServer = new TestServer(builder);
            Client = _testServer.CreateClient();

            //Seeds
            DbContext = _testServer.Services.GetService<OngDbContext>();
            DbContext.Roles.AddRange(DataAccess.Seeds.RoleSeed.GetData());
            DbContext.Users.AddRange(DataAccess.Seeds.UserSeed.GetData());
            DbContext.SaveChanges();

            //Tokens
            var controller = "auth";

            //Credentials
            var credentialsAdmin = new { email = "MartaJuarez@gmail.com", password = "123456" };
            var credentialsUser = new { email = "CarlosJuarez@gmail.com", password = "123456" };

            var jsonAdmin = JsonConvert.SerializeObject(credentialsAdmin);
            var jsonUser = JsonConvert.SerializeObject(credentialsUser);

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