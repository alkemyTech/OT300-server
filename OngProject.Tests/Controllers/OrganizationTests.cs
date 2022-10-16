using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
//using System.Text.Json;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OngProject.Tests.Controllers
{
    [TestClass]
    public class OrganizationTests
    {
        string controller;

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
            var token =   new AuthenticationHeaderValue("Bearer", Init.TokenUser);
            Init.Client.DefaultRequestHeaders.Authorization = token;

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
            //  new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);

            Assert.Fail("not implemented");

        }

        [TestMethod]
        public async Task Public_UpdatePublicData_as_Admin_Missing_Name_Fail()
        {
            //  new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);

            Assert.Fail("not implemented");

        }

        [TestMethod]
        public async Task Public_UpdatePublicData_as_Admin_Missing_Image_Fail()
        {
            //  new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);

            Assert.Fail("not implemented");

        }

        [TestMethod]
        public async Task Public_UpdatePublicData_as_Admin_Missing_Email_Fail()
        {
            //  new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);

            Assert.Fail("not implemented");

        }

        [TestMethod]
        public async Task Public_UpdatePublicData_as_Admin_Missing_WelcomeText_Fail()
        {
            //  new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);

            Assert.Fail("not implemented");

        }

        [TestMethod]
        public async Task Public_UpdatePublicData_as_Admin_Missing_Facebook_Fail()
        {
            //  new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);

            Assert.Fail("not implemented");

        }

        [TestMethod]
        public async Task Public_UpdatePublicData_as_Admin_Missing_Instagram_Fail()
        {
            //  new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);

            Assert.Fail("not implemented");

        }

        [TestMethod]
        public async Task Public_UpdatePublicData_as_Admin_Missing_Linkedin_Fail()
        {
            //  new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);

            Assert.Fail("not implemented");

        }

        [TestCleanup]
        public void CleanTest()
        {
            Init.Client.DefaultRequestHeaders.Authorization = null;
        }
    }

    class PublicResponse
    {
        public OrganizationPublicDTO orgPubInfoDTO { get; set; }
        public List<SlidePublicDTO> slidesOrganizations { get; set; }
    }



}
