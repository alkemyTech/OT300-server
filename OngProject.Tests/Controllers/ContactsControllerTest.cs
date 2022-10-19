using Microsoft.VisualStudio.TestTools.UnitTesting;
using OngProject.Tests;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OngProject.Tests.Controllers
{
    [TestClass]
    public class ContactsControllerTest
    {
        
        static byte[] byteArrayImage;
        [ClassInitialize]
        public static async Task ClassInit(TestContext ctx)
        {
            
            // seeds
            Init.DbContext.Contacts.AddRange(DataAccess.Seeds.ContactSeed.GetData());
            
            await Init.DbContext.SaveChangesAsync();

            var imageBas64 = "/9j/4AAQSkZJRgABAQEASABIAAD/2wBDAAYEBQYFBAYGBQYHBwYIChAKCgkJChQODwwQFxQYGBcU\r\nFhYaHSUfGhsjHBYWICwgIyYnKSopGR8tMC0oMCUoKSj/2wBDAQcHBwoIChMKChMoGhYaKCgoKCgo\r\nKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCj/wAARCAAKAAoDASIA\r\nAhEBAxEB/8QAFgABAQEAAAAAAAAAAAAAAAAABgcI/8QAJxAAAQMDAgQHAAAAAAAAAAAAAQIDBAAF\r\nEQYHEiExQRMUIzNCUVP/xAAVAQEBAAAAAAAAAAAAAAAAAAABAv/EABgRAAMBAQAAAAAAAAAAAAAA\r\nAAABAjEy/9oADAMBAAIRAxEAPwBRs1a5Nx0Bp65Iuz0KOU+IYcdPC0Alw5GM9FcJzy7mpDrV6OrW\r\nV+IW0QZ8g9R+iqZ7YTJLe2FvbbkPJbEZeEpWQBzV2rP82Oz5x/0W/cV8R91ERMcgklh//9k=";
            byteArrayImage = Convert.FromBase64String(imageBas64);
        }
        [TestMethod]
        public async Task GetAllContactAsUser_Fail()
        {
           
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenUser);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            var result = await Init.Client.GetAsync("/api/Contacts");

            Assert.AreEqual(403, (int)result.StatusCode);
        }

        [TestMethod]
        public async Task GetAllContactAsAdmin_Success()
        {

            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            var result = await Init.Client.GetAsync("/api/Contacts");

            Assert.AreEqual(200, (int)result.StatusCode);
            Assert.IsNotNull(result.Content);
        }


        // Agregar contacto como usuario success

        [TestMethod]
        public async Task AddContactAsUser_Success()
        {

            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenUser);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            object newcontact = new
            {
                Name = "Diego",
                Phone = 2223333,
                Email = "dgojmnz@gmail.com",
                Message = "dgojimenez000"
            };

            using StringContent jsoncontact = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(newcontact),
                Encoding.UTF8,
                "application/json");

            var result = await Init.Client.PostAsync("/api/Contacts", jsoncontact);

            Assert.AreEqual(201, (int)result.StatusCode);
        }

        // Agregar contacto como admin success
        [TestMethod]
        public async Task AddContactAsAdmin_Success()
        {

            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            object newcontact = new
            {
                Name = "Diego",
                Phone = 2223333,
                Email = "dgojmnz@gmail.com",
                Message = "dgojimenez000"
            };

            using StringContent jsoncontact = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(newcontact),
                Encoding.UTF8,
                "application/json");

            var result = await Init.Client.PostAsync("/api/Contacts", jsoncontact);

            Assert.AreEqual(201, (int)result.StatusCode);
        }

        // Agregar contacto con un dato faltante Fail
        [TestMethod]
        public async Task AddContactAsAdminMissingARequiredField_Fail()
        {

            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            object newcontact = new
            {
              
                Phone = 2223333,
                Email = "dgojmnz@gmail.com",
                Message = "dgojimenez000"
            };

            using StringContent jsoncontact = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(newcontact),
                Encoding.UTF8,
                "application/json");

            var result = await Init.Client.PostAsync("/api/Contacts", jsoncontact);

            Assert.AreEqual(400, (int)result.StatusCode);
        }
        [TestMethod]
        public async Task AddContactAsAdminMissingANoRequiredField_Success()
        {

            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            object newcontact = new
            {
                Name = "Diego",
                Email = "dgojmnz@gmail.com",
                Message = "dgojimenez000"
            };

            using StringContent jsoncontact = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(newcontact),
                Encoding.UTF8,
                "application/json");

            var result = await Init.Client.PostAsync("/api/Contacts", jsoncontact);

            Assert.AreEqual(201, (int)result.StatusCode);
        }
    }
}
