
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
using OngProject.Core.Models;

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



        /// <summary>
        /// Gets the list of categories names
        /// </summary>
        /// <param name="page"></param>
        /// <returns>A list of categories names</returns>
        /// <remarks>
        /// Sample Request:
        /// api/category/page=2
        /// </remarks>
        /// <response code="200">The list of names</response>
        /// /// <response code="400">If page number does not exist</response>
        // GET: api/<CategoriesController>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
                PagedList<CategoryDTO> paged = _categoryBusiness.GetAll(page.Value);
                if(page > paged.TotalPages)
                {
                    return BadRequest($"page number {page} doesn't exist");
                }
                else
                {
                    var url = this.Request.Path;
                    return Ok(new
                    {
                        next = paged.HasNext ? $"{url}/{page + 1}" : "",
                        prev = (paged.Count > 0 && paged.HasPrevious) ? $"{url}/{page - 1}" : "",
                        currentPage = paged.CurrentPage,
                        totalPages = paged.TotalPages,
                        data = paged
                    });
                }
                
            }
        }

        /// <summary>
        /// Gets the details for a unique Category
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The details for the category with the same id</returns>
        /// <remarks>
        /// Sample Request:
        ///  Get Category/id/1
        /// </remarks>
        /// <response code="200">The category Details</response>
        /// /// <response code="404">If Category does not exist</response>
        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryFullDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Get(int id)
        {
            var exists = await _categoryBusiness.GetById(id);
            if (exists == null || exists.Id == 0) return NotFound("No category with such id");
            return Ok(exists);
        }

        /// <summary>
        /// Creates a new Category
        /// </summary>
        /// <param name="createCategoryDTO"></param>
        /// <returns>The Created Category</returns>
        /// <remarks>
        /// Sample Request:
        ///     POST /Category
        ///     {
        ///        "Description": "Educations Activities",
        ///        "name": "Education",
        ///        "imageFile": "Image to uppload and save"
        ///     }
        /// </remarks>
        /// <response code="201">The category Details</response>
        /// /// <response code="409">If Category Already exist</response>
        // POST api/<CategoriesController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CategoryFullDTO))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> Post([Bind(new string[] { "Name", "Description" })][FromForm] CategoryPostDTO createCategoryDTO, IFormFile imageFile)
        {
            createCategoryDTO.File = imageFile?.OpenReadStream();

            CategoryFullDTO created = await _categoryBusiness.Add(createCategoryDTO);

            return (created.Id == 0) ?
                 Conflict($"Category with  name : {createCategoryDTO.Name} already exists") :
                 Created("", created);
        }

        /// <summary>
        /// Updates an existing Category
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The Updated Category</returns>
        /// <remarks>
        /// Sample Request:
        ///     PUT /Category/2
        ///     {
        ///        "Description": "Educations Activities",
        ///        "name": "Education",
        ///        "imageFile": "Image to uppload and save"
        ///     }
        /// </remarks>
        /// <response code="200">The updated category Details</response>
        /// /// <response code="400">If Category does not exist</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryFullDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromForm] CategoryPostDTO categoryPostDTO, IFormFile file, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (file != null) { categoryPostDTO.File = file.OpenReadStream(); } else { categoryPostDTO.File = null; }
            var response = await _categoryBusiness.UpdateCategory(id, categoryPostDTO);
            return Ok(response);
        }

        /// <summary>
        /// Deletes an existing Category
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Empty response</returns>
        /// <remarks>
        /// Sample Request:
        ///     Delete /Category/2
        /// </remarks>
        /// <response code="200">If category Was deleted</response>
        /// /// <response code="404">If Category does not exist</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
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