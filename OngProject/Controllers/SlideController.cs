using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlideController : ControllerBase
    {
        private readonly ISlideBusiness _service;

        public SlideController(ISlideBusiness service)
        {
            _service = service;
        }



        // GET: api/<SlidesController>
        [HttpGet]
        public IEnumerable<SlideDTO> Get()
        {
           return _service.GetAll();
        }

        // GET api/<SlidesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SlidesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SlidesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SlidesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
