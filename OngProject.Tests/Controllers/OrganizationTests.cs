using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
//using System.Text.Json;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public async Task SetupTests()
        {
            controller = "organization";
        }

        //"/api/organization/public
        [TestMethod]
        public async Task Public_Should_Return_PublicInfo_From_Org()
        {
            //Arrange


            //Act
            var response = await Init.Client.GetAsync($"/api/{controller}/public");
            var actualJson = await response.Content.ReadAsStringAsync();

            JsonTextReader reader = new JsonTextReader(new StringReader(actualJson));

            var actualObj =
               JsonSerializer.Create().Deserialize<PublicResponse>(reader);

            //Expected
            var orgEntity = await Init.DbContext.Organizations.FirstAsync();
            var slidedEntity = await Init.DbContext.Slides.Where( x => x.OrganizationId == orgEntity.Id).OrderBy( o => o.Order).ToListAsync();

            var expectedOrg = orgEntity.ToPublicDTO();
            var expectedSlides = slidedEntity.Select( x => x.ToPublicDTO()).ToList();

            PublicResponse expectedObj = new() { orgPubInfoDTO = expectedOrg, slidesOrganizations = expectedSlides };


            //StringBuilder sb = new StringBuilder();
            //StringWriter sw = new StringWriter(sb);

            //JsonWriter writer = new JsonTextWriter(sw);

            var expectedJson = JsonConvert.SerializeObject(expectedObj);

            //Assert
            Assert.AreEqual(expectedJson.ToLower(), actualJson.ToLower());
        }


        public async Task Public_UpdatePublicData_as_Visitor_Fail()
        {
            //  new AuthenticationHeaderValue("Bearer", Init.TokenUser);
        }

        public async Task Public_UpdatePublicData_as_User_Fail()
        {
            //  new AuthenticationHeaderValue("Bearer", Init.TokenUser);
        }

        public async Task Public_UpdatePublicData_as_Admin_Success()
        {
            //  new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);

        }
    }

    class PublicResponse
    {
        public OrganizationPublicDTO orgPubInfoDTO { get; set; }
        public List<SlidePublicDTO> slidesOrganizations { get; set; }
    }



}
