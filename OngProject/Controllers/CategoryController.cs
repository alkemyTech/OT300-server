
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
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Extensions;
using OngProject.Repositories;

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
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetAllNames([FromQuery] int? page = 1)
        {
            var role = (HttpContext.User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.Role).Value;

            //if admin US-48
            if (role.Equals("Admin"))
            {
                return Ok(_categoryBusiness.GetAllCatNames());
            }
            //if user  US-94
            else
            {
                var url = this.Request.Path;
                PagedList<CategoryDTO> paged = _categoryBusiness.GetAll(page.Value);
                return Ok(new
                {
                    next = paged.HasNext ? $"{url}/{page + 1}" : "",
                    prev = (paged.Count > 0 && paged.HasPrevious) ? $"{url}/{page - 1}" : "",
                    currentPage = paged.CurrentPage,
                    totalPages = paged.TotalPages,
                    data = paged,
                });
            }
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
            dto.File = imageFile?.OpenReadStream();

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
            var doesExist = await _categoryBusiness.DoesExist(id);

            if (!doesExist)
            {
                return NotFound("Either we couldn't find that category or we're having a problem");
            }

            await _categoryBusiness.Delete(id);

            return Ok();
        }
    }
}