using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User, Admin")]
    public class SlideController : ControllerBase
    {
        private readonly ISlideBusiness _service;

        public SlideController(ISlideBusiness service)
        {
            _service = service;
        }



        // GET: api/<SlidesController>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IEnumerable<SlideDTO> Get()
        {
           return _service.GetAll();
        }

        // GET api/<SlidesController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SlidesController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SlidesController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SlidesController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public void Delete(int id)
        {
        }
    }
}
