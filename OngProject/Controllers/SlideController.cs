using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using System.Threading.Tasks;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User, Admin")]
    public class SlideController : ControllerBase
    {
        private readonly ISlideBusiness _slideBusiness;

        public SlideController(ISlideBusiness slideBusiness)
        {
            _slideBusiness = slideBusiness;
        }



        // GET: api/<SlidesController>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Get()
        {
           return Ok(_slideBusiness.GetAll());
        }

        // GET api/<SlidesController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetById(int id)
        {
            return Ok("value");
        }

        // POST api/<SlidesController>
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm] SlideCreateDTO createRequest)
        {
            var slide = await _slideBusiness.Create(createRequest);

           return CreatedAtAction(nameof(GetById), slide.Id, slide);
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