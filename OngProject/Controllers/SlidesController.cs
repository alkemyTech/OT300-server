using Microsoft.AspNetCore.Mvc;
using OngProject.Repositories.Interfaces;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlidesController : ControllerBase
    {
        private readonly ISlidesBusiness _service;

        public SlidesController(ISlidesBusiness service)
        {
            _service = service;
        }



        // GET: api/<SlidesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
