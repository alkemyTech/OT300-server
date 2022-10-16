using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
//using System.Text.Json;
using OngProject.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OngProject.Tests.Controllers
{
    [TestClass]
    public class OrganizationTests
    {
        string controller;
        static MultipartFormDataContent formContent;
        static byte[] byteArrayImage;

        [ClassInitialize]
        public static async Task ClassInit(TestContext ctx)
        {
            // seeds
            Init.DbContext.Slides.AddRange(DataAccess.Seeds.SlideSeed.GetData());
            Init.DbContext.Organizations.AddRange(DataAccess.Seeds.OrganizationSeed.GetData());
            await Init.DbContext.SaveChangesAsync();

            var imageBas64 = "/9j/4AAQSkZJRgABAQEASABIAAD/2wBDAAYEBQYFBAYGBQYHBwYIChAKCgkJChQODwwQFxQYGBcU\r\nFhYaHSUfGhsjHBYWICwgIyYnKSopGR8tMC0oMCUoKSj/2wBDAQcHBwoIChMKChMoGhYaKCgoKCgo\r\nKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCj/wAARCAAKAAoDASIA\r\nAhEBAxEB/8QAFgABAQEAAAAAAAAAAAAAAAAABgcI/8QAJxAAAQMDAgQHAAAAAAAAAAAAAQIDBAAF\r\nEQYHEiExQRMUIzNCUVP/xAAVAQEBAAAAAAAAAAAAAAAAAAABAv/EABgRAAMBAQAAAAAAAAAAAAAA\r\nAAABAjEy/9oADAMBAAIRAxEAPwBRs1a5Nx0Bp65Iuz0KOU+IYcdPC0Alw5GM9FcJzy7mpDrV6OrW\r\nV+IW0QZ8g9R+iqZ7YTJLe2FvbbkPJbEZeEpWQBzV2rP82Oz5x/0W/cV8R91ERMcgklh//9k=";
            byteArrayImage = Convert.FromBase64String(imageBas64);
        }

        [TestInitialize]
        public void SetupTests()
        {
            controller = "organization";

        }

        /// <summary>
        /// US-30
        /// US-69
        /// US-73
        /// </summary>
        /// <returns></returns>
        //GET"/api/organization/public
        [TestMethod]
        public async Task Public_ShouldReturnSpecific_Fields()
        {
            //Arrange
            var properties = new[] { "name", "img", "phonenumber", "adress", "facebookurl", "linkedinurl", "instagramurl" };

            //Act
            var response = await Init.Client.GetAsync($"/api/{controller}/public");
            var actualJson = await response.Content.ReadAsStringAsync();

            var expConverter = new ExpandoObjectConverter();
            dynamic obj = JsonConvert.DeserializeObject<ExpandoObject>(actualJson, expConverter);
            dynamic ong = obj.orgPubInfoDTO;

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);

            foreach (dynamic item in ong)
            {
                string key = item.Key;
                if (!properties.Contains(key.ToLower())) Assert.Fail(key);
            }
        }

        /// <summary>
        /// US-69
        /// </summary>
        /// <returns></returns>
        //GET"/api/organization/public
        [TestMethod]
        public async Task Public_Slides_Should_come_in_order()
        {
            //Arrange
            var response = await Init.Client.GetAsync($"/api/{controller}/public");
            var actualJson = await response.Content.ReadAsStringAsync();

            var expConverter = new ExpandoObjectConverter();
            dynamic obj = JsonConvert.DeserializeObject<ExpandoObject>(actualJson, expConverter);

            //Act
            dynamic slides = obj.slidesOrganizations;

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
            int order = 0;
            foreach (dynamic item in slides)
            {
                if (item.order >= order) order = (int)item.order;
                else Assert.Fail($"slides order fail at [{item.text}]");
            }
        }

        /// <summary>
        /// US-69
        /// </summary>
        [TestMethod]
        public async Task Public_Slides_Are_From_Org()
        {
            //Arrange
            //make  it fail
            //  await Init.DbContext.Slides.AddAsync(new Entities.Slide() { Id = 50, Text = "failing Slide", Order = 20, OrganizationId = 20 });
            //await Init.DbContext.Slides.AddAsync(new Entities.Slide() { Id = 51, Text = "failing Slide", Order = 20 });
            //await Init.DbContext.SaveChangesAsync();

            //Expected
            var orgEntity = await Init.DbContext.Organizations.FirstAsync();

            //Act
            //Actual
            var response = await Init.Client.GetAsync($"/api/{controller}/public");
            var actualJson = await response.Content.ReadAsStringAsync();

            JsonTextReader reader = new JsonTextReader(new StringReader(actualJson));
            PublicResponse actualObj = JsonSerializer.Create().Deserialize<PublicResponse>(reader);

            //Slides are from org
            var slideNotFromOrg = actualObj.slidesOrganizations.FirstOrDefault(s => s.OrganizationId != orgEntity.Id);

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.IsTrue(slideNotFromOrg == null, $"not all slides are from organization");
        }

        /// <summary>
        /// US-45
        /// </summary>
        // GET
        [TestMethod]
        public async Task Public_UpdatePublicData_as_Visitor_Fail()
        {
            //Arrange
            var formContent =
                new FormUrlEncodedContent(
                    new[]
                    {
                        new KeyValuePair<string, string>("name", ""),
                        new KeyValuePair<string, string>("email", "")
                     });

            //Act
            var response = await Init.Client.PostAsync($"/api/{controller}/public", formContent);
            //var actualJson = await response.Content.ReadAsStringAsync();
            Console.WriteLine(response.StatusCode);

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Unauthorized);
        }

        /// <summary>
        /// US-45
        /// </summary>
        /// 
        [TestMethod]
        public async Task Public_UpdatePublicData_as_User_Fail()
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
                        new KeyValuePair<string, string>("email", "")
                     });

            //Act
            var response = await Init.Client.PostAsync($"/api/{controller}/public", formContent);
            //var actualJson = await response.Content.ReadAsStringAsync();
            Console.WriteLine(response.StatusCode);

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Forbidden);
        }

        /// <summary>
        /// US-45
        /// </summary>
        /// 
        [TestMethod]
        public async Task Public_UpdatePublicData_as_Admin_Success()
        {
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            //create invalid content
            formContent = new MultipartFormDataContent();

            using var fileContent = new ByteArrayContent(byteArrayImage);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
            formContent.Add(fileContent, "Image", "image.jpg");

            formContent.Add(new StringContent("newName"), "Name");
            formContent.Add(new StringContent("123456"), "PhoneNumber");
            formContent.Add(new StringContent("name"), "adress");
            formContent.Add(new StringContent("http://url1.com"), "FacebookUrl");
            formContent.Add(new StringContent("http://url2.com"), "InstagramUrl");
            formContent.Add(new StringContent("http://url3.com"), "linkedinurl");

            //Act
            var response = await Init.Client.PostAsync($"/api/{controller}/public", formContent);
            var actualJson = await response.Content.ReadAsStringAsync();

            //updated result from public endpoint
            var updatedResponse = await Init.Client.GetAsync($"/api/{controller}/public");
            var updatedJson = await updatedResponse.Content.ReadAsStringAsync();

            JsonTextReader reader = new JsonTextReader(new StringReader(updatedJson));
            OrganizationPublicDTO expectedObj = JsonSerializer.Create().Deserialize<PublicResponse>(reader).orgPubInfoDTO;

            //updated result from database
            //var expectedObj = Init.DbContext.Organizations.First();


            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.AreEqual(expectedObj.Name, "newName");
            Assert.AreEqual(expectedObj.PhoneNumber, 123456);
            Assert.AreEqual(expectedObj.Adress, "name");
            Assert.AreEqual(expectedObj.FacebookUrl, "http://url1.com");
            Assert.AreEqual(expectedObj.InstagramUrl, "http://url2.com");
            Assert.AreEqual(expectedObj.LinkedInUrl, "http://url3.com");
            Assert.IsTrue(expectedObj.Img.Contains("url-mocked"));
        }

        [TestMethod]
        public async Task Public_UpdatePublicData_as_Admin_Missing_Name_Fail()
        {
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            //create invalid content
            formContent = new MultipartFormDataContent();

            using var fileContent = new ByteArrayContent(byteArrayImage);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
            formContent.Add(fileContent, "Image", "image.jpg");

            // formContent.Add(new StringContent("name"), "Name");
            formContent.Add(new StringContent("123456"), "PhoneNumber");
            formContent.Add(new StringContent("name"), "adress");
            formContent.Add(new StringContent("http://url.com"), "FacebookUrl");
            formContent.Add(new StringContent("http://url.com"), "InstagramUrl");
            formContent.Add(new StringContent("http://url.com"), "linkedinurl");


            //Act
            var response = await Init.Client.PostAsync($"/api/{controller}/public", formContent);
            var actualJson = await response.Content.ReadAsStringAsync();


            var expConverter = new ExpandoObjectConverter();
            dynamic obj = JsonConvert.DeserializeObject<ExpandoObject>(actualJson, expConverter);

            //errors must not be null
            dynamic errors = obj.errors ?? null;
            if (errors == null) Assert.Fail("No errors");

            List<KeyValuePair<string, object>> listErrors = Enumerable.ToList(errors);

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
            Assert.IsTrue(listErrors.Count == 1);
            //error must be name
            Assert.IsTrue(listErrors.ElementAt(0).Key == "Name"); ;
        }

        [TestMethod]
        public async Task Public_UpdatePublicData_as_Admin_Missing_Image_Fail()
        {
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            //create invalid content
            formContent = new MultipartFormDataContent();

            //using var fileContent = new ByteArrayContent(byteArrayImage);
            //fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
            // formContent.Add(fileContent, "Image", "image.jpg");

            formContent.Add(new StringContent("name"), "Name");
            formContent.Add(new StringContent("123456"), "PhoneNumber");
            formContent.Add(new StringContent("name"), "adress");
            formContent.Add(new StringContent("http://url.com"), "FacebookUrl");
            formContent.Add(new StringContent("http://url.com"), "InstagramUrl");
            formContent.Add(new StringContent("http://url.com"), "linkedinurl");


            //Act
            var response = await Init.Client.PostAsync($"/api/{controller}/public", formContent);
            var actualJson = await response.Content.ReadAsStringAsync();


            var expConverter = new ExpandoObjectConverter();
            dynamic obj = JsonConvert.DeserializeObject<ExpandoObject>(actualJson, expConverter);

            //errors must not be null
            dynamic errors = obj.errors ?? null;
            if (errors == null) Assert.Fail("No errors");

            List<KeyValuePair<string, object>> listErrors = Enumerable.ToList(errors);

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
            Assert.IsTrue(listErrors.Count == 1);
            //error must be name
            Assert.IsTrue(listErrors.ElementAt(0).Key == "Image");
        }





        [TestMethod]
        public async Task Public_UpdatePublicData_as_Admin_Missing_Facebook_Fail()
        {
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            //create invalid content
            formContent = new MultipartFormDataContent();

            using var fileContent = new ByteArrayContent(byteArrayImage);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
            formContent.Add(fileContent, "Image", "image.jpg");

            formContent.Add(new StringContent("name"), "Name");
            formContent.Add(new StringContent("123456"), "PhoneNumber");
            formContent.Add(new StringContent("name"), "adress");
            // formContent.Add(new StringContent("http://url.com"), "FacebookUrl");
            formContent.Add(new StringContent("http://url.com"), "InstagramUrl");
            formContent.Add(new StringContent("http://url.com"), "linkedinurl");

            //Act
            var response = await Init.Client.PostAsync($"/api/{controller}/public", formContent);
            var actualJson = await response.Content.ReadAsStringAsync();
            var expConverter = new ExpandoObjectConverter();
            dynamic obj = JsonConvert.DeserializeObject<ExpandoObject>(actualJson, expConverter);

            //errors must not be null
            dynamic errors = obj.errors ?? null;
            if (errors == null) Assert.Fail("No errors");

            List<KeyValuePair<string, object>> listErrors = Enumerable.ToList(errors);

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
            Assert.IsTrue(listErrors.Count == 1);
            //error must be name
            Assert.IsTrue(listErrors.ElementAt(0).Key == "FacebookUrl");
        }

        [TestMethod]
        public async Task Public_UpdatePublicData_as_Admin_Invalid_Url_Facebook_Fail()
        {
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            //create invalid content
            formContent = new MultipartFormDataContent();

            using var fileContent = new ByteArrayContent(byteArrayImage);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
            formContent.Add(fileContent, "Image", "image.jpg");

            formContent.Add(new StringContent("name"), "Name");
            formContent.Add(new StringContent("123456"), "PhoneNumber");
            formContent.Add(new StringContent("name"), "adress");
            formContent.Add(new StringContent("invalidUrl"), "FacebookUrl");
            formContent.Add(new StringContent("http://url.com"), "InstagramUrl");
            formContent.Add(new StringContent("http://url.com"), "linkedinurl");

            //Act
            var response = await Init.Client.PostAsync($"/api/{controller}/public", formContent);
            var actualJson = await response.Content.ReadAsStringAsync();
            var expConverter = new ExpandoObjectConverter();
            dynamic obj = JsonConvert.DeserializeObject<ExpandoObject>(actualJson, expConverter);

            //errors must not be null
            dynamic errors = obj.errors ?? null;
            if (errors == null) Assert.Fail("No errors");

            List<KeyValuePair<string, object>> listErrors = Enumerable.ToList(errors);

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
            Assert.IsTrue(listErrors.Count == 1);
            //error must be name
            Assert.IsTrue(listErrors.ElementAt(0).Key == "FacebookUrl");
        }

        [TestMethod]
        public async Task Public_UpdatePublicData_as_Admin_Missing_Instagram_Fail()
        {
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            //create invalid content
            formContent = new MultipartFormDataContent();

            using var fileContent = new ByteArrayContent(byteArrayImage);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
            formContent.Add(fileContent, "Image", "image.jpg");

            formContent.Add(new StringContent("name"), "Name");
            formContent.Add(new StringContent("123456"), "PhoneNumber");
            formContent.Add(new StringContent("name"), "adress");
            formContent.Add(new StringContent("http://url.com"), "FacebookUrl");
            // formContent.Add(new StringContent("http://url.com"), "InstagramUrl");
            formContent.Add(new StringContent("http://url.com"), "linkedinurl");

            //Act
            var response = await Init.Client.PostAsync($"/api/{controller}/public", formContent);
            var actualJson = await response.Content.ReadAsStringAsync();
            var expConverter = new ExpandoObjectConverter();
            dynamic obj = JsonConvert.DeserializeObject<ExpandoObject>(actualJson, expConverter);

            //errors must not be null
            dynamic errors = obj.errors ?? null;
            if (errors == null) Assert.Fail("No errors");

            List<KeyValuePair<string, object>> listErrors = Enumerable.ToList(errors);

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
            Assert.IsTrue(listErrors.Count == 1);
            //error must be name
            Assert.IsTrue(listErrors.ElementAt(0).Key == "InstagramUrl");
        }

        [TestMethod]
        public async Task Public_UpdatePublicData_as_Admin_Invalid_URL_Instagram_Fail()
        {

            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            //create invalid content
            formContent = new MultipartFormDataContent();

            using var fileContent = new ByteArrayContent(byteArrayImage);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
            formContent.Add(fileContent, "Image", "image.jpg");

            formContent.Add(new StringContent("name"), "Name");
            formContent.Add(new StringContent("123456"), "PhoneNumber");
            formContent.Add(new StringContent("name"), "adress");
            formContent.Add(new StringContent("http://url.com"), "FacebookUrl");
            formContent.Add(new StringContent("invalidUrl"), "InstagramUrl");
            formContent.Add(new StringContent("http://url.com"), "linkedinurl");

            //Act
            var response = await Init.Client.PostAsync($"/api/{controller}/public", formContent);
            var actualJson = await response.Content.ReadAsStringAsync();
            var expConverter = new ExpandoObjectConverter();
            dynamic obj = JsonConvert.DeserializeObject<ExpandoObject>(actualJson, expConverter);

            //errors must not be null
            dynamic errors = obj.errors ?? null;
            if (errors == null) Assert.Fail("No errors");

            List<KeyValuePair<string, object>> listErrors = Enumerable.ToList(errors);

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
            Assert.IsTrue(listErrors.Count == 1);
            //error must be name
            Assert.IsTrue(listErrors.ElementAt(0).Key == "InstagramUrl");
        }

        [TestMethod]
        public async Task Public_UpdatePublicData_as_Admin_Missing_Linkedin_Fail()
        {

            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            //create invalid content
            formContent = new MultipartFormDataContent();

            using var fileContent = new ByteArrayContent(byteArrayImage);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
            formContent.Add(fileContent, "Image", "image.jpg");

            formContent.Add(new StringContent("name"), "Name");
            formContent.Add(new StringContent("123456"), "PhoneNumber");
            formContent.Add(new StringContent("name"), "adress");
            formContent.Add(new StringContent("http://url.com"), "FacebookUrl");
            formContent.Add(new StringContent("http://url.com"), "InstagramUrl");
            // formContent.Add(new StringContent("http://url.com"), "linkedinurl");

            //Act
            var response = await Init.Client.PostAsync($"/api/{controller}/public", formContent);
            var actualJson = await response.Content.ReadAsStringAsync();
            var expConverter = new ExpandoObjectConverter();
            dynamic obj = JsonConvert.DeserializeObject<ExpandoObject>(actualJson, expConverter);

            //errors must not be null
            dynamic errors = obj.errors ?? null;
            if (errors == null) Assert.Fail("No errors");

            List<KeyValuePair<string, object>> listErrors = Enumerable.ToList(errors);

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
            Assert.IsTrue(listErrors.Count == 1);
            //error must be name
            Assert.IsTrue(listErrors.ElementAt(0).Key == "LinkedInUrl");
        }

        [TestMethod]
        public async Task Public_UpdatePublicData_as_Admin_Invalid_URL_Linkedin_Fail()
        {
            //Arrange
            var token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
            Init.Client.DefaultRequestHeaders.Authorization = token;

            //create invalid content
            formContent = new MultipartFormDataContent();

            using var fileContent = new ByteArrayContent(byteArrayImage);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
            formContent.Add(fileContent, "Image", "image.jpg");

            formContent.Add(new StringContent("name"), "Name");
            formContent.Add(new StringContent("123456"), "PhoneNumber");
            formContent.Add(new StringContent("name"), "adress");
            formContent.Add(new StringContent("http://url.com"), "FacebookUrl");
            formContent.Add(new StringContent("http://url.com"), "InstagramUrl");
            formContent.Add(new StringContent("invalidUrl"), "LinkedInUrl");

            //Act
            var response = await Init.Client.PostAsync($"/api/{controller}/public", formContent);
            var actualJson = await response.Content.ReadAsStringAsync();
            var expConverter = new ExpandoObjectConverter();
            dynamic obj = JsonConvert.DeserializeObject<ExpandoObject>(actualJson, expConverter);

            //errors must not be null
            dynamic errors = obj.errors ?? null;
            if (errors == null) Assert.Fail("No errors");

            List<KeyValuePair<string, object>> listErrors = Enumerable.ToList(errors);

            //Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
            Assert.IsTrue(listErrors.Count == 1);
            //error must be name
            Assert.IsTrue(listErrors.ElementAt(0).Key == "LinkedInUrl");
        }

        [TestCleanup]
        public void CleanTest()
        {
            Init.Client.DefaultRequestHeaders.Authorization = null;
            //formContent.Dispose();
        }
    }

    class PublicResponse
    {
        public OrganizationPublicDTO orgPubInfoDTO { get; set; }
        public List<SlidePublicDTO> slidesOrganizations { get; set; }
    }
}
