
using OngProject.Core.Interfaces;

﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User, Admin")]
    public class CategoryController : ControllerBase
    {
        
        
        private readonly ICategoryBusiness _categoryBusiness;

        public CategoryController(ICategoryBusiness categoryBusiness)
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
        public void Post([FromBody] string value)
        {
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
