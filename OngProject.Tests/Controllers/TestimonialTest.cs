using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json.Linq;
using OngProject.Core.Interfaces;
using OngProject.Entities;

namespace OngProject.Tests.Controllers
{
	[TestClass]
	public class TestimonialTest
	{
		private string _controller;
		private static byte[] _byteArrayImage;
		private ITestimonialBusiness _testimonialBusiness;

		[ClassInitialize]
		public static async Task ClassInit(TestContext ctx)
		{
			//Seeds
			await Init.DbContext.Testimonials.AddRangeAsync(DataAccess.Seeds.TestimonialSeed.GetData());
			await Init.DbContext.SaveChangesAsync();

			var imageBas64 = "/9j/4AAQSkZJRgABAQEASABIAAD/2wBDAAYEBQYFBAYGBQYHBwYIChAKCgkJChQODwwQFxQYGBcU\r\nFhYaHSUfGhsjHBYWICwgIyYnKSopGR8tMC0oMCUoKSj/2wBDAQcHBwoIChMKChMoGhYaKCgoKCgo\r\nKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCj/wAARCAAKAAoDASIA\r\nAhEBAxEB/8QAFgABAQEAAAAAAAAAAAAAAAAABgcI/8QAJxAAAQMDAgQHAAAAAAAAAAAAAQIDBAAF\r\nEQYHEiExQRMUIzNCUVP/xAAVAQEBAAAAAAAAAAAAAAAAAAABAv/EABgRAAMBAQAAAAAAAAAAAAAA\r\nAAABAjEy/9oADAMBAAIRAxEAPwBRs1a5Nx0Bp65Iuz0KOU+IYcdPC0Alw5GM9FcJzy7mpDrV6OrW\r\nV+IW0QZ8g9R+iqZ7YTJLe2FvbbkPJbEZeEpWQBzV2rP82Oz5x/0W/cV8R91ERMcgklh//9k=";
			_byteArrayImage = Convert.FromBase64String(imageBas64);
		}


		[TestInitialize]
		public void Setup()
		{
			_controller = "testimonial";
		}

		[TestMethod]
		public async Task Get_NumberPageIsNegative_ShouldThrowException()
		{
			//Arrange
			bool exceptionThrown = false;
			AuthenticationHeaderValue token = new AuthenticationHeaderValue("Bearer", Init.TokenUser);
			Init.Client.DefaultRequestHeaders.Authorization = token;

			//Act
			try
			{
				HttpResponseMessage response = await Init.Client.GetAsync($"api/{_controller}?page=-1");
			}
			catch (ArgumentException)
			{
				exceptionThrown = true;
			}			

			// Assert
			Assert.IsTrue(exceptionThrown);
		}

		[TestMethod]
		public async Task Get_NumberPageIsGreaterThanTotal_ShouldReturnBadRequest()
		{
			//Arrange
			AuthenticationHeaderValue token = new AuthenticationHeaderValue("Bearer", Init.TokenUser);
			Init.Client.DefaultRequestHeaders.Authorization = token;

			//Act
			HttpResponseMessage response = await Init.Client.GetAsync($"api/{_controller}?page=9000");

			//Assert
			Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
		}

		[TestMethod]
		public async Task Get_AllTestimonials_ShouldSuccess()
		{
			//Arrange
			AuthenticationHeaderValue token = new AuthenticationHeaderValue("Bearer", Init.TokenUser);
			Init.Client.DefaultRequestHeaders.Authorization = token;

			//Act
			HttpResponseMessage response = await Init.Client.GetAsync($"api/{_controller}?page=1");

			//Assert
			Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
			Assert.IsNotNull(response.Content);
		}


		[TestMethod]
		public async Task Update_AsUser_ShouldFail()
		{
			//Arrange
			AuthenticationHeaderValue token = new AuthenticationHeaderValue("Bearer", Init.TokenUser);
			Init.Client.DefaultRequestHeaders.Authorization = token;

			// Create content
			MultipartFormDataContent formContent = new MultipartFormDataContent();
			using ByteArrayContent fileContent = new ByteArrayContent(_byteArrayImage);
			
			fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

			formContent.Add(new StringContent("newName"), "Name");
			formContent.Add(fileContent, "Image", "Image.jpg");
			formContent.Add(new StringContent("newContent"), "Content");

			//Act
			HttpResponseMessage response = await Init.Client.PutAsync($"api/{_controller}/1", formContent);

			//Assert
			Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Forbidden);
		}

		[TestMethod]
		public async Task Update_AsAdmin_ShouldSuccess()
		{
			//Arrange
			AuthenticationHeaderValue token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
			Init.Client.DefaultRequestHeaders.Authorization = token;

			// Create content
			MultipartFormDataContent formContent = new MultipartFormDataContent();
			using ByteArrayContent fileContent = new ByteArrayContent(_byteArrayImage);

			fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

			formContent.Add(new StringContent("newName"), "Name");
			formContent.Add(fileContent, "Image", "Image.jpg");
			formContent.Add(new StringContent("newContent"), "Content");

			//Act
			HttpResponseMessage response = await Init.Client.PutAsync($"api/{_controller}/1", formContent);
			string content = await response.Content.ReadAsStringAsync();
			JObject jsonContent = JObject.Parse(content);

			//Assert
			Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
			Assert.AreEqual("newName", jsonContent["name"]);
			Assert.AreEqual("newContent", jsonContent["content"]);
		}

		[TestMethod]
		public async Task Update_NotExistingTestimonial_ShouldReturnNotFound()
		{
			//Arrange
			AuthenticationHeaderValue token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
			Init.Client.DefaultRequestHeaders.Authorization = token;

			// Create content
			MultipartFormDataContent formContent = new MultipartFormDataContent();
			using ByteArrayContent fileContent = new ByteArrayContent(_byteArrayImage);

			fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

			formContent.Add(new StringContent("newName"), "Name");
			formContent.Add(fileContent, "Image", "Image.jpg");
			formContent.Add(new StringContent("newContent"), "Content");

			//Act
			HttpResponseMessage response = await Init.Client.PutAsync($"api/{_controller}/901290", formContent);

			//Assert
			Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.NotFound);
		}

		[TestMethod]
		public async Task Post_AsUser_ShouldFail()
		{
			//Arrange
			AuthenticationHeaderValue token = new AuthenticationHeaderValue("Bearer", Init.TokenUser);
			Init.Client.DefaultRequestHeaders.Authorization = token;

			// Create content
			MultipartFormDataContent formContent = new MultipartFormDataContent();
			using ByteArrayContent fileContent = new ByteArrayContent(_byteArrayImage);

			fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

			formContent.Add(new StringContent("newName"), "Name");
			formContent.Add(fileContent, "Image", "Image.jpg");
			formContent.Add(new StringContent("newContent"), "Content");

			//Act
			HttpResponseMessage response = await Init.Client.PostAsync($"api/{_controller}", formContent);
			Console.WriteLine(response.StatusCode);

			//Assert
			Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Forbidden);

		}

		[TestMethod]
		public async Task Post_NameIsNull_ShouldReturnBadRequest()
		{
			//Arrange
			AuthenticationHeaderValue token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
			Init.Client.DefaultRequestHeaders.Authorization = token;

			// Create content
			MultipartFormDataContent formContent = new MultipartFormDataContent();
			using ByteArrayContent fileContent = new ByteArrayContent(_byteArrayImage);

			fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

			formContent.Add(fileContent, "Image", "Image.jpg");
			formContent.Add(new StringContent("newContent"), "Content");

			//Act

			HttpResponseMessage response = await Init.Client.PostAsync($"api/{_controller}", formContent);

			//Assert

			Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
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
			Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Forbidden);
		}



		[TestMethod]
		public async Task Delete_AsAdmin_ShouldSuccess()
		{
			//Arrange
			AuthenticationHeaderValue token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
			Init.Client.DefaultRequestHeaders.Authorization = token;

			//Act
			HttpResponseMessage response = await Init.Client.DeleteAsync($"api/{_controller}/1");
			string content = await response.Content.ReadAsStringAsync();
			//Assert

			Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
			Assert.AreEqual("true", content);
		}

		public async Task Delete_TestimonialDoesNotExist_ShouldThrowException()
		{
			//Arrange
			bool exceptionThrown = false;
			AuthenticationHeaderValue token = new AuthenticationHeaderValue("Bearer", Init.TokenAdmin);
			Init.Client.DefaultRequestHeaders.Authorization = token;

			//Act
			try
			{
				HttpResponseMessage response = await Init.Client.DeleteAsync($"api/{_controller}/118193");
			}
			catch (Exception)
			{
				exceptionThrown = true;
			}

			//Assert

			Assert.IsTrue(exceptionThrown);
		}

	}
}
