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
    public class UnitTestNews
    {
        string controller;
        static MultipartFormDataContent newsFormContent;
        static byte[] byteArrayImage;
        private OngDbContext _ctx;
        RepositoryBase<News> repoNews;


        [ClassInitialize]
        public static async Task ClassInit(TestContext ctx)
        {
            // seeds
            Init.DbContext.News.AddRange(OngProject.DataAccess.Seeds.NewsSeed.GetData());
            Init.DbContext.Comments.AddRange(OngProject.DataAccess.Seeds.CommentSeed.GetData());
            Init.DbContext.Categories.AddRange(OngProject.DataAccess.Seeds.CategorySeed.GetData());
            await Init.DbContext.SaveChangesAsync();

            var imageBas64 = "/9j/4AAQSkZJRgABAQEASABIAAD/2wBDAAYEBQYFBAYGBQYHBwYIChAKCgkJChQODwwQFxQYGBcU\r\nFhYaHSUfGhsjHBYWICwgIyYnKSopGR8tMC0oMCUoKSj/2wBDAQcHBwoIChMKChMoGhYaKCgoKCgo\r\nKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCj/wAARCAAKAAoDASIA\r\nAhEBAxEB/8QAFgABAQEAAAAAAAAAAAAAAAAABgcI/8QAJxAAAQMDAgQHAAAAAAAAAAAAAQIDBAAF\r\nEQYHEiExQRMUIzNCUVP/xAAVAQEBAAAAAAAAAAAAAAAAAAABAv/EABgRAAMBAQAAAAAAAAAAAAAA\r\nAAABAjEy/9oADAMBAAIRAxEAPwBRs1a5Nx0Bp65Iuz0KOU+IYcdPC0Alw5GM9FcJzy7mpDrV6OrW\r\nV+IW0QZ8g9R+iqZ7YTJLe2FvbbkPJbEZeEpWQBzV2rP82Oz5x/0W/cV8R91ERMcgklh//9k=";
            byteArrayImage = Convert.FromBase64String(imageBas64);
        }

        [TestInitialize]
        public void SetupTests()
        {
            controller = "/api/News";

        }

        /* TEST:
         * 
         * 1-A GetAll_AsAnonymous_Fail -
         * 1-B GetAll_AsUser_Success -
         * 1-C GetAll_AsAdmin_Success -
         * 2-A GetById_AsUser_Fail -
         * 2-B GetById_AsAdmin_Success -
         * 2-C GetById_AsAdminIncorrectId_Fail -
         * 3-A GetAllCommentsFromNewsId_AsAnonymous_Fail -
         * 3-B GetAllCommentsFromNewsId_AsUser_Success -
         * 3-C GetAllCommentsFromNewsId_AsAdmin_Success -
         * 3-D AllCommentsAreFromNewsId_Success -
         * 4-A Post_AsUser_Fail -
         * 4-B Post_AsAdmin_Success -
         * 4-C Post_AsAdminWithOneFieldMissing_Fail -
         * 5-A Update_AsUser_Fail -
         * 5-B Update_AsAdmin_Success -
         * 5-C Update_AsAdminMissingCategory_Fail -
         * 5-D Update_AsAdminIncorrectId_Fail -
         * 6-A Delete_AsUser_Fail -  
         * 6-B Delete_AsAdmin_Success
         * 
         */

        [TestMethod]
        public async Task GetAll_AsAnonymous_Fail()
        {   
            var response = await Init.Client.GetAsync(controller);
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Unauthorized);            
        }

        [TestMethod]
        public async Task GetAll_AsUser_Success()
        {
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenUser);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            var response = await Init.Client.GetAsync(controller);
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task GetAll_AsAdmin_Success()
        {
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            var response = await Init.Client.GetAsync(controller);
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
        }

        [TestMethod]
        [DataRow(4)]
        public async Task GetById_AsUser_Fail(int id)
        {
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenUser);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            var response = await Init.Client.GetAsync($"{controller}/{id}");
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Forbidden);

        }

        [TestMethod]
        [DataRow(4)]
        public async Task GetById_AsAdmin_Success(int id)
        {
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            var properties = new[] { "id", "name", "content", "image", "idcategory", "createdat", "isdeleted", "lasteditedat" };

            //Act
            var response = await Init.Client.GetAsync($"{controller}/{id}");
            var actualJson = await response.Content.ReadAsStringAsync();

            var expConverter = new ExpandoObjectConverter();
            dynamic obj = JsonConvert.DeserializeObject<ExpandoObject>(actualJson, expConverter);
            
            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);

            foreach (dynamic item in obj)
            {
                string key = item.Key;
                if (!properties.Contains(key.ToLower()))
                {
                    Console.WriteLine(key);
                    Assert.Fail(key); 
                }
            }

        }

        [TestMethod]
        [DataRow(25)]
        public async Task GetById_AsAdminIncorrectId_Fail(int id)
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
        [DataRow(2)]
        public async Task GetAllCommentsFromNewsId_AsAnonymous_Fail(int id)
        {
            //Arrange
            //Act
            var response = await Init.Client.GetAsync($"{controller}/{id}/comment");

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Unauthorized);

        }

        [TestMethod]
        [DataRow(2)]
        public async Task GetAllCommentsFromNewsId_AsUser_Success(int id)
        {
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenUser);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            //Act
            var response = await Init.Client.GetAsync($"{controller}/{id}/comment");

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);

        }

        [TestMethod]
        [DataRow(2)]
        public async Task GetAllCommentsFromNewsId_AsAdmin_Success(int id)
        {
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            //Act
            var response = await Init.Client.GetAsync($"{controller}/{id}/comment");

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
                        
        }

        [TestMethod]
        [DataRow(2)]
        public async Task AllCommentsAreFromNewsId_Success(int id)
        {
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            var newsEntity = await Init.DbContext.News.FindAsync(id);

            //Act
            var response = await Init.Client.GetAsync($"{controller}/{id}/comment");
            var responseJson = await response.Content.ReadAsStringAsync();

            JsonTextReader reader = new JsonTextReader(new StringReader(responseJson));
            List<CommentAddDto> actualObj = JsonSerializer.Create().Deserialize<List<CommentAddDto>>(reader);

            var commentsNotFromNews = actualObj.FirstOrDefault(c => c.NewsId != newsEntity.Id) ;

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.IsNull(commentsNotFromNews);

        }

        [TestMethod]
        [DataRow(5)]
        public async Task PostAndUpdate_AsUser_Fail(int id)
        {
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenUser);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            //some content
            var formContent =
                new FormUrlEncodedContent(
                    new[]
                    {
                        new KeyValuePair<string, string>("name", ""),
                        new KeyValuePair<string, string>("content", ""),
                        new KeyValuePair<string, string>("image", ""),
                        new KeyValuePair<string, string>("idcategory", "5")
                     });

            //Act
            var responsePost = await Init.Client.PostAsync(controller, formContent);
            var responsePut = await Init.Client.PutAsync($"{controller}/{id}", formContent);
            
            //Assert
            Assert.IsTrue(responsePost.StatusCode == System.Net.HttpStatusCode.Forbidden);
            Assert.IsTrue(responsePut.StatusCode == System.Net.HttpStatusCode.Forbidden);
        }

        [TestMethod]
        public async Task Post_AsAdmin_Success()
        {
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            //create
            newsFormContent = new MultipartFormDataContent();

            using var fileContent = new ByteArrayContent(byteArrayImage);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");           

            newsFormContent.Add(new StringContent("testing2"), "name");
            newsFormContent.Add(new StringContent("6"), "idCategory");
            newsFormContent.Add(new StringContent("content Testing"), "content");
            newsFormContent.Add(fileContent, "imageFile", "image.jpg");


            //Act
            var response = await Init.Client.PostAsync(controller, newsFormContent);
            var actualJson = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Created);
        }

        [TestMethod]
        public async Task Post_AsAdminWithOneFieldMissing_Fail()
        {
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            //create
            MultipartFormDataContent imageMissingFormContent = new MultipartFormDataContent();
            MultipartFormDataContent nameMissingFormContent = new MultipartFormDataContent();
            MultipartFormDataContent contentMissingFormContent = new MultipartFormDataContent();
            MultipartFormDataContent categoryMissingFormContent = new MultipartFormDataContent();

            using var fileContent = new ByteArrayContent(byteArrayImage);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

            imageMissingFormContent.Add(new StringContent("testing2"), "name");
            imageMissingFormContent.Add(new StringContent("6"), "idCategory");
            imageMissingFormContent.Add(new StringContent("content Testing"), "content");

            nameMissingFormContent.Add(new StringContent("6"), "idCategory");
            nameMissingFormContent.Add(new StringContent("content Testing"), "content");
            nameMissingFormContent.Add(fileContent, "imageFile", "image.jpg");

            contentMissingFormContent.Add(new StringContent("testing2"), "name");
            contentMissingFormContent.Add(new StringContent("6"), "idCategory");
            contentMissingFormContent.Add(fileContent, "imageFile", "image.jpg");

            categoryMissingFormContent.Add(new StringContent("testing2"), "name");
            categoryMissingFormContent.Add(new StringContent("content Testing"), "content");
            categoryMissingFormContent.Add(fileContent, "imageFile", "image.jpg");

            //Act
            var responseImage = await Init.Client.PostAsync(controller, imageMissingFormContent);
            var responseName = await Init.Client.PostAsync(controller, nameMissingFormContent);
            var responseContent = await Init.Client.PostAsync(controller, contentMissingFormContent);
            var responseCategory = await Init.Client.PostAsync(controller, categoryMissingFormContent);


            //Assert
            Assert.IsTrue(responseImage.StatusCode == System.Net.HttpStatusCode.BadRequest);
            Assert.IsTrue(responseName.StatusCode == System.Net.HttpStatusCode.BadRequest);
            Assert.IsTrue(responseContent.StatusCode == System.Net.HttpStatusCode.BadRequest);
            Assert.IsTrue(responseCategory.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }

        [TestMethod]
        [DataRow(8)]
        public async Task Update_AsAdmin_Success(int id)
        {
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            //Act
            newsFormContent = new MultipartFormDataContent();

            newsFormContent.Add(new StringContent(""), "name");
            newsFormContent.Add(new StringContent("7"), "idCategory");
            newsFormContent.Add(new StringContent(""), "content");

            var response = await Init.Client.PutAsync($"{controller}/{id}", newsFormContent);

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
        }

        [TestMethod]
        [DataRow(8)]
        public async Task Update_AsAdminMissingCategory_Fail(int id)
        {
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            //Act
            newsFormContent = new MultipartFormDataContent();

            newsFormContent.Add(new StringContent(""), "name");
            newsFormContent.Add(new StringContent(""), "idCategory");
            newsFormContent.Add(new StringContent(""), "content");

            var response = await Init.Client.PutAsync($"{controller}/{id}", newsFormContent);

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }

        [TestMethod]
        [DataRow(25)]
        public async Task Update_AsAdminIncorrectId_Fail(int id)
        {
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            //Act
            newsFormContent = new MultipartFormDataContent();

            newsFormContent.Add(new StringContent(""), "name");
            newsFormContent.Add(new StringContent("7"), "idCategory");
            newsFormContent.Add(new StringContent(""), "content");

            var response = await Init.Client.PutAsync($"{controller}/{id}", newsFormContent);

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.NotFound);
        }

        [TestMethod]
        [DataRow(7)]
        public async Task Delete_AsUser_Fail(int id)
        {
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenUser);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            //Act
            var response = await Init.Client.DeleteAsync($"{controller}/{id}");

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Forbidden);
        }

        [TestMethod]
        [DataRow(7)]
        public async Task Delete_AsAdmin_Success(int id)
        {
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            //Act
            var response = await Init.Client.DeleteAsync($"{controller}/{id}");

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
        }

        [TestMethod]
        [DataRow(25)]
        public async Task Delete_AsAdminIncorrectId_Fail(int id)
        {
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            //Act
            var response = await Init.Client.DeleteAsync($"{controller}/{id}");

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.NotFound);
        }

        [TestCleanup]
        public void CleanTest()
        {
            Init.Client.DefaultRequestHeaders.Authorization = null;
            //newsFormContent.Dispose();
        }

    }

    public class NewsResponse
    {
        public NewsDTO newsDTO { get; set; }
        public List<CommentAddDto> commentsDto { get; set; }
    }
}
