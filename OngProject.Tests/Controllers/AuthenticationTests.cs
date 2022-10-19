using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OngProject.Core.Models.DTOs;

namespace OngProject.Tests.Controllers
{
    [TestClass]
    public class AuthenticationTests
    {
        private string controller;
        private MultipartFormDataContent formContent;
        private static byte[] byteArrayImage;

        [ClassInitialize]
        public static async Task ClassInit(TestContext ctx)
        {
            // seeds
            Init.DbContext.Slides.AddRange(DataAccess.Seeds.SlideSeed.GetData());
            Init.DbContext.Organizations.AddRange(DataAccess.Seeds.OrganizationSeed.GetData());
            await Init.DbContext.SaveChangesAsync();
        }

        [TestInitialize]
        public void SetupTests()
        {
            controller = "Auth";
        }

        [TestMethod]
        public async Task LoginShould_return200OkAndToken_WhenValidCredentials()
        {
            //Arrange
            var loginDto = new UserLoginDTO()
            {
                Email = "MartaJuarez@gmail.com",
                Password = "123456"
            };

            //Act
            var result = await Init.Client.PostAsJsonAsync($"/api/{controller}/Login", loginDto);
            var resultJson = await result.Content.ReadAsStringAsync();

            //Assert
            Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task LoginShould_return401Forbidden_WhenInValidCredentials()
        {
            //Arrange
            var loginDto = new UserLoginDTO()
            {
                Email = "MRandonarez@gmail.com",
                Password = "12Randon3456"
            };

            //Act
            var result = await Init.Client.PostAsJsonAsync($"/api/{controller}/Login", loginDto);

            //Assert
            Assert.IsTrue(result.StatusCode == HttpStatusCode.Unauthorized);
        }

        [TestMethod]
        public async Task RegisterShould_return404BadRequest_WhenAlreadyUsedEmail()
        {
            //Arrange
            var expectedMessage = "There's an user registered with that email. Please try another one.";
            var registerRequest = new RegisterDTO()
            {
                Email = "MartaJuarez@gmail.com",
                Password = "qweqweqwe",
                FirstName = "testing",
                LastName = "tejjst"
            };

            //Act
            var result = await Init.Client.PostAsJsonAsync($"/api/{controller}/Register", registerRequest);
            var responseJson = await result.Content.ReadAsStringAsync();

            //Assert
            Assert.IsTrue(result.StatusCode == HttpStatusCode.BadRequest);
            Assert.AreEqual(expectedMessage, responseJson);
        }

        [TestMethod]
        public async Task RegisterShould_returnCreated_WhenValidData()
        {
            //Arrange
            var registerRequest = new RegisterDTO()
            {
                Email = "artaJuarez@gmail.com",
                Password = "TestPassword",
                FirstName = "TestName",
                LastName = "TestLastName"
            };

            //Act
            var result = await Init.Client.PostAsJsonAsync($"/api/{controller}/Register", registerRequest);
            var responseJson = await result.Content.ReadAsStringAsync();
            var createdUser = Init.DbContext.Users.SingleOrDefault(x => x.FirstName == "TestName");

            //Assert
            Assert.IsTrue(result.StatusCode == HttpStatusCode.Created);
            Assert.IsNotNull(createdUser);
            Assert.AreNotEqual(createdUser.Password, registerRequest.Password);
        }

        [TestMethod]
        public async Task AuthMeShould_returnUserDetails_WhenValidToken()
        {
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;
            var actualUser = Init.DbContext.Users.SingleOrDefault(x => x.Email == "MartaJuarez@gmail.com");

            //Act
            var result = await Init.Client.GetAsync($"/api/{controller}/me");
            var responseJson = await result.Content.ReadAsStringAsync();
            var responseArray = responseJson.Split(",");
            var id = responseArray[0].Split(":")[1];

            //Assert
            Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
            Assert.IsNotNull(responseJson);
            Assert.AreEqual(id, actualUser.Id.ToString());
        }

        [TestMethod]
        public async Task AuthMe_ShouldReturnUnauthorize_WhenInValidToken()
        {
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin + "qwe");
            Init.Client.DefaultRequestHeaders.Authorization = token;

            //Act
            var result = await Init.Client.GetAsync($"/api/{controller}/me");
            var responseJson = await result.Content.ReadAsStringAsync();

            //Assert
            Assert.IsTrue(result.StatusCode == HttpStatusCode.Unauthorized);
        }
    }
}