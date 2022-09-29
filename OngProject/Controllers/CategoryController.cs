
using OngProject.Core.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using OngProject.Core.Models.DTOs;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using OngProject.Core.Helper;
using System.IO;
using System;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User, Admin")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryBusiness _categoryBusiness;

        public CategoryController(ICategoryBusiness categoryBusiness, IImageStorageHerlper imageHelper)
        {
            _categoryBusiness = categoryBusiness;
        }


        // GET: api/<CategoriesController>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllNames()
        {
            return Ok(_categoryBusiness.GetAllCatNames());
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CategoriesController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Post([Bind(new string[] { "Name", "Description" })][FromForm] CategoryPostDTO dto, IFormFile imageFile)
        {
            var stream = imageFile.OpenReadStream();

            CategoryFullDTO created = await _categoryBusiness.Add(dto, stream);

            return (created.Id == 0) ?
                 Conflict($"Category with  name : {dto.Name} already exists") :
                 Created("", created);
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
