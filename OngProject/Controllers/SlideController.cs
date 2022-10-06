using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using System.Threading.Tasks;
using OngProject.Core.Mapper;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;


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
        public async Task<IActionResult> GetById(int id)
        {
            var slide = await _slideBusiness.GetById(id);
            if (slide is null)
            {
                return NotFound("Slide with given id does not exist");
            }

            return Ok(slide.ToSlideResponseDTO());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(201, Type = typeof(SlideResponseDTO))]
        public async Task<IActionResult> Create([FromForm] SlideCreateDTO createRequest)
        {
            var slide = await _slideBusiness.Create(createRequest);

            return Created("", slide);
        }

        // PUT api/<SlidesController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, [FromForm] SlideCreateDTO dto)
        {
            return await _slideBusiness.Update(id, dto) ? Ok():NotFound() ;
        }

        // DELETE api/<SlidesController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await _slideBusiness.DoesExist(id);

            if (!exist)
            {
                return NotFound("Either we couldn't find that slide or we're having a problem");
            }
            else
            {
                await _slideBusiness.RemoveSlide(id);
                return Ok();
            }
            

        }
    }
}