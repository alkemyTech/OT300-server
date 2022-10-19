using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using OngProject.Controllers;
using OngProject.Core.Business;
using OngProject.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OngProject.Tests.Controllers
{
    [TestClass]
    public class MemberTests
    {
		private string _controller;
		private static byte[] _byteArrayImage;
		static MultipartFormDataContent newsFormContent;


		[ClassInitialize]
		public static async Task ClassInit(TestContext ctx)
		{
			//Seeds
			await Init.DbContext.Members.AddRangeAsync(DataAccess.Seeds.MemberSeed.GetData());
			await Init.DbContext.SaveChangesAsync();

			var imageBas64 = "/9j/4AAQSkZJRgABAQEASABIAAD/2wBDAAYEBQYFBAYGBQYHBwYIChAKCgkJChQODwwQFxQYGBcU" +
				"\r\nFhYaHSUfGhsjHBYWICwgIyYnKSopGR8tMC0oMCUoKSj/2wBDAQcHBwoIChMKChMoGhYaKCgoKCgo" +
				"\r\nKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCj/wAARCAAKAAoDASIA" +
				"\r\nAhEBAxEB/8QAFgABAQEAAAAAAAAAAAAAAAAABgcI/8QAJxAAAQMDAgQHAAAAAAAAAAAAAQIDBAAF" +
				"\r\nEQYHEiExQRMUIzNCUVP/xAAVAQEBAAAAAAAAAAAAAAAAAAABAv/EABgRAAMBAQAAAAAAAAAAAAAA" +
				"\r\nAAABAjEy/9oADAMBAAIRAxEAPwBRs1a5Nx0Bp65Iuz0KOU+IYcdPC0Alw5GM9FcJzy7mpDrV6OrW" +
				"\r\nV+IW0QZ8g9R+iqZ7YTJLe2FvbbkPJbEZeEpWQBzV2rP82Oz5x/0W/cV8R91ERMcgklh//9k=";

			_byteArrayImage = Convert.FromBase64String(imageBas64);
		}

		[TestInitialize]
		public void Setup()
		{
			_controller = "member";

		}

		[TestMethod]
		public async Task Get_NumberPageIsNegative_Fail()
		{
			int page = -1;

			AuthenticationHeaderValue token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
			Init.Client.DefaultRequestHeaders.Authorization = token;

			var response = await Init.Client.GetAsync($"api/{_controller}{page}");

			Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.NotFound);

		}

		[TestMethod]
		public async Task Get_NumberPage_Success()
		{
			AuthenticationHeaderValue token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
			Init.Client.DefaultRequestHeaders.Authorization = token;

			var response = await Init.Client.GetAsync($"api/{_controller}?page");

			Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);			
		}

		[TestMethod]
		public async Task Get_NumberPage_AsUser_Fail()
		{
			AuthenticationHeaderValue token = new AuthenticationHeaderValue("Bearer", Init.TokenUser);
			Init.Client.DefaultRequestHeaders.Authorization = token;

			var response = await Init.Client.GetAsync($"api/{_controller}?page");

			Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Forbidden);
		}

		
	}
}
