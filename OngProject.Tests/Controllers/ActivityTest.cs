using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Dynamic;
using OngProject.Core.Models.DTOs;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;

namespace OngProject.Tests.Controllers
{

    [TestClass]
    public class ActivityTest
    {

        string controller;
        static MultipartFormDataContent ActivityFormContent;
        static byte[] byteArrayImage;   


        [ClassInitialize]
        public static async Task ClassInit(TestContext ctx)
        {
            // seeds
            Init.DbContext.Activities.AddRange(DataAccess.Seeds.ActivitiesSeed.GetData());
            await Init.DbContext.SaveChangesAsync();

            var imageBas64 = "/9j/4AAQSkZJRgABAQEASABIAAD/2wBDAAYEBQYFBAYGBQYHBwYIChAKCgkJChQODwwQFxQYGBcU\r\nFhYaHSUfGhsjHBYWICwgIyYnKSopGR8tMC0oMCUoKSj/2wBDAQcHBwoIChMKChMoGhYaKCgoKCgo\r\nKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCj/wAARCAAKAAoDASIA\r\nAhEBAxEB/8QAFgABAQEAAAAAAAAAAAAAAAAABgcI/8QAJxAAAQMDAgQHAAAAAAAAAAAAAQIDBAAF\r\nEQYHEiExQRMUIzNCUVP/xAAVAQEBAAAAAAAAAAAAAAAAAAABAv/EABgRAAMBAQAAAAAAAAAAAAAA\r\nAAABAjEy/9oADAMBAAIRAxEAPwBRs1a5Nx0Bp65Iuz0KOU+IYcdPC0Alw5GM9FcJzy7mpDrV6OrW\r\nV+IW0QZ8g9R+iqZ7YTJLe2FvbbkPJbEZeEpWQBzV2rP82Oz5x/0W/cV8R91ERMcgklh//9k=";
            byteArrayImage = Convert.FromBase64String(imageBas64);
        }

        [TestInitialize]
        public void SetupTests()
        {
            controller = "/api/Activity";

        }

        /*
         * TEST
         * 
         * 1-A GetByIdAsAnonymous_Fail() -
         * 1-B GetByIdAsUser_Success() -
         * 1-C GetByIdAsAdmin_Success() -
         * 1-D GetById_IncorrectId_Fail() -
         * 2-A PostAsUser_Fail() -
         * 2-B PostAsAdmin_Success() -
         * 2-C Post_MissingField_Fail() -
         * 3-A PutAsUser_Fail() -
         * 3-B PutAsAdmin_Success() - 
         * 3-C Put_MissingField_Success() -
         * 
         */


        [TestMethod]
        [DataRow(5)]
        public async Task GetByIdAsAnonymous_Fail(int id)
        {
            //Arrange
            //Act
            var response = await Init.Client.GetAsync($"{controller}/{id}");

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Unauthorized);
        }

        [TestMethod]
        [DataRow(5)]
        public async Task GetByIdAsUser_Success(int id)
        {
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenUser);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            //Act
            var response = await Init.Client.GetAsync($"{controller}/{id}");

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
        }

        [TestMethod]
        [DataRow(5)]
        public async Task GetByIdAsAdmin_Success(int id)
        {
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            //Act
            var response = await Init.Client.GetAsync($"{controller}/{id}");

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
        }

        [TestMethod]
        [DataRow(50)]
        public async Task GetById_IncorrectId_Fail(int id)
        {
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            //Act
            var response = await Init.Client.GetAsync($"{controller}/{id}");

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.NotFound);
        }

        [TestMethod]
        public async Task PostAsUser_Fail()
        {
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenUser);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            //create
            ActivityFormContent = new MultipartFormDataContent();

            using var fileContent = new ByteArrayContent(byteArrayImage);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

            ActivityFormContent.Add(new StringContent("testing2"), "name");            
            ActivityFormContent.Add(new StringContent("content Testing"), "content");
            ActivityFormContent.Add(fileContent, "imageFile", "image.jpg");


            //Act
            var response = await Init.Client.PostAsync(controller, ActivityFormContent);
            

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Forbidden);
        }

        [TestMethod]
        public async Task PostAsAdmin_Success()
        {
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            //create
            ActivityFormContent = new MultipartFormDataContent();

            using var fileContent = new ByteArrayContent(byteArrayImage);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

            ActivityFormContent.Add(new StringContent("testing2"), "name");
            ActivityFormContent.Add(new StringContent("content Testing"), "content");
            ActivityFormContent.Add(fileContent, "imageFile", "image.jpg");


            //Act
            var response = await Init.Client.PostAsync(controller, ActivityFormContent);


            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Created);
        }

        [TestMethod]
        public async Task Post_MissingField_Fail()
        {
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            //create
            ActivityFormContent = new MultipartFormDataContent();

            using var fileContent = new ByteArrayContent(byteArrayImage);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

            
            ActivityFormContent.Add(new StringContent("content Testing"), "content");
            ActivityFormContent.Add(fileContent, "imageFile", "image.jpg");


            //Act
            var response = await Init.Client.PostAsync(controller, ActivityFormContent);


            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }
                
        [TestMethod]
        [DataRow(8)]
        public async Task PutAsUser_Fail(int id)
        {
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenUser);
            Init.Client.DefaultRequestHeaders.Authorization = token;
                        
            ActivityFormContent = new MultipartFormDataContent();

            using var fileContent = new ByteArrayContent(byteArrayImage);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

            ActivityFormContent.Add(new StringContent("pruebaupdate"), "name");            
            ActivityFormContent.Add(new StringContent("testing content update"), "content");
            ActivityFormContent.Add(fileContent, "imageFile", "image.jpg");

            //Act
            var response = await Init.Client.PutAsync($"{controller}/{id}", ActivityFormContent);

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Forbidden);
        }

        [TestMethod]
        [DataRow(8)]
        public async Task PutAsAdmin_Success(int id)
        {
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            ActivityFormContent = new MultipartFormDataContent();

            using var fileContent = new ByteArrayContent(byteArrayImage);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

            ActivityFormContent.Add(new StringContent("pruebaupdate"), "name");
            ActivityFormContent.Add(new StringContent("testing content update"), "content");
            ActivityFormContent.Add(fileContent, "imageFile", "image.jpg");

            //Act
            var response = await Init.Client.PutAsync($"{controller}/{id}", ActivityFormContent);

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
        }

        [TestMethod]
        [DataRow(8)]
        public async Task Put_MissingField_Success(int id)
        {
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            MultipartFormDataContent ActivityFormContentName = new MultipartFormDataContent();
            MultipartFormDataContent ActivityFormContentContent = new MultipartFormDataContent();
            MultipartFormDataContent ActivityFormContentImage = new MultipartFormDataContent();

            using var fileContent = new ByteArrayContent(byteArrayImage);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

            ActivityFormContentName.Add(new StringContent("testing content update"), "content");
            ActivityFormContentName.Add(fileContent, "imageFile", "image.jpg");

            ActivityFormContentContent.Add(new StringContent("pruebaupdate"), "name");
            ActivityFormContentContent.Add(fileContent, "imageFile", "image.jpg");

            ActivityFormContentImage.Add(new StringContent("pruebaupdate"), "name");
            ActivityFormContentImage.Add(new StringContent("testing content update"), "content");
            

            //Act
            var responseName = await Init.Client.PutAsync($"{controller}/{id}", ActivityFormContent);
            var responseContent = await Init.Client.PutAsync($"{controller}/{id}", ActivityFormContent);
            var responseImage = await Init.Client.PutAsync($"{controller}/{id}", ActivityFormContent);

            //Assert
            Assert.IsTrue(responseName.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.IsTrue(responseContent.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.IsTrue(responseImage.StatusCode == System.Net.HttpStatusCode.OK);
        }
    }
}
