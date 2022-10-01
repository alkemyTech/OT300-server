
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
        public async Task<ActionResult> Get(int id)
        {
            var exists = await _categoryBusiness.GetById(id);
            if (exists == null || exists.Id == 0) return NotFound("No category with such id");
            return Ok(exists);
        }

        // POST api/<CategoriesController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Post([Bind(new string[] { "Name", "Description" })][FromForm] CategoryPostDTO dto, IFormFile imageFile)
        {
            dto.File = imageFile!=null?imageFile.OpenReadStream():new MemoryStream();

            CategoryFullDTO created = await _categoryBusiness.Add(dto);

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
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _categoryBusiness.Delete(id);
            return result?NoContent():NotFound("Either we couldn't find that category or we're having a problem");
        }
    }
}
