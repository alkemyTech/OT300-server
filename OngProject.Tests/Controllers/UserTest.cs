using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OngProject.Tests.Controllers
{
    [TestClass]
    public class UserTest
    {
        private string _controller;

        [TestInitialize]
        public void Setup()
        {
            _controller = "user";
        }

        [TestMethod]
        public async Task Get_AsUser_ShouldReturnForbidden()
        {
            //Arrange
            AuthenticationHeaderValue token = new AuthenticationHeaderValue("Bearer", Init.TokenUser);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            //Act
            HttpResponseMessage response = await Init.Client.GetAsync($"api/{_controller}");

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Forbidden);
        }

        [TestMethod]
        public async Task Get_AsAdmin_ShouldReturnOK_And_ShouldReturnNotNullContent()
        {
            // Arrange
            AuthenticationHeaderValue token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            //Act
            HttpResponseMessage response = await Init.Client.GetAsync($"api/{_controller}");

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.IsNotNull(response.Content);
        }

        [TestMethod]
        public async Task Update_MyUser_AsUser_ShouldSuccess()
        {
            //Arrange
            AuthenticationHeaderValue token = new AuthenticationHeaderValue("Bearer", Init.TokenUser);
            Init.Client.DefaultRequestHeaders.Authorization = token;
            
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityToken jsonToken = handler.ReadToken(Init.TokenUser);
            JwtSecurityToken securityToken = jsonToken as JwtSecurityToken;

            int id = int.Parse(securityToken.Claims.First(claim => claim.Type == "Identifier").Value);

            object content = new
            {
                firstName = "Diego",
                lastName = "Jimenez",
                email = "dgojmnz@gmail.com",
                password = "dgojimenez000"
            };

            using StringContent jsonContent = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(content), 
                Encoding.UTF8, 
                "application/json");

            //Act
            HttpResponseMessage response = await Init.Client.PatchAsync($"api/{_controller}/{id}", jsonContent);

            // Store the updated information
            string updated = await response.Content.ReadAsStringAsync();
            JObject jsonUpdated = JObject.Parse(updated);

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);

            Assert.AreEqual("Diego", jsonUpdated["firstName"]);
            Assert.AreEqual("Jimenez", jsonUpdated["lastName"]);
            Assert.AreEqual("dgojmnz@gmail.com", jsonUpdated["email"]);
            Assert.AreEqual("dgojimenez000", jsonUpdated["password"]);
        }

        [TestMethod]
        public async Task Update_AnotherUser_AsUser_ShouldFail()
        {
            //Arrange
            AuthenticationHeaderValue token = new AuthenticationHeaderValue("Bearer", Init.TokenUser);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            object content = new
            {
                firstName = "Javier",
                lastName = "Hernandez",
                email = "javierhernandez@gmail.com",
                password = "javih123"
            };

            using StringContent jsonContent = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(content),
                Encoding.UTF8,
                "application/json");

            //Act
            HttpResponseMessage response = await Init.Client.PatchAsync($"api/{_controller}/11", jsonContent);

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Forbidden);
        }


        [TestMethod]
        public async Task Update_AnotherUser_AsAdmin_ShouldSuccess()
        {
            //Arrange
            AuthenticationHeaderValue token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            object content = new
            {
                firstName = "Javier",
                lastName = "Hernandez",
                email = "javierhernandez@gmail.com",
                password = "javih123"
            };

            using StringContent jsonContent = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(content),
                Encoding.UTF8,
                "application/json");
            //Act
            HttpResponseMessage response = await Init.Client.PatchAsync($"api/{_controller}/11", jsonContent);

            // Store the updated information
            string updated = await response.Content.ReadAsStringAsync();
            JObject jsonUpdated = JObject.Parse(updated);

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);

            Assert.AreEqual("Javier", jsonUpdated["firstName"]);
            Assert.AreEqual("Hernandez", jsonUpdated["lastName"]);
            Assert.AreEqual("javierhernandez@gmail.com", jsonUpdated["email"]);
            Assert.AreEqual("javih123", jsonUpdated["password"]);
        }

        [TestMethod]
        public async Task Update_NotExistingUser_ShouldReturnNotFound()
        {
            //Arrange
            AuthenticationHeaderValue token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            object content = new
            {
                firstName = "Javier",
                lastName = "Hernandez",
                email = "javierhernandez@gmail.com",
                password = "javih123"
            };

            using StringContent jsonContent = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(content),
                Encoding.UTF8,
                "application/json");

            //Act
            HttpResponseMessage response = await Init.Client.PatchAsync($"api/{_controller}/921", jsonContent);
            Console.WriteLine(response.StatusCode);
            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.NotFound);
        }

        [TestMethod]
        public async Task Delete_AsUser_ShouldFail()
        {
            //Arrange
            AuthenticationHeaderValue token = new AuthenticationHeaderValue("Bearer", Init.TokenUser);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            //Act
            HttpResponseMessage response = await Init.Client.DeleteAsync($"api/{_controller}/1");

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task Delete_AsAdmin_ShouldSuccess()
        {
            //Arrange
            AuthenticationHeaderValue token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            //Act
            HttpResponseMessage response = await Init.Client.DeleteAsync($"api/{_controller}/1");

            //Assert

            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
        }
    }
}

