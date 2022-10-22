using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using System.Threading.Tasks;
using OngProject.Core.Mapper;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using OngProject.Entities;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class SlideController : ControllerBase
    {
        private readonly ISlideBusiness _slideBusiness;

        public SlideController(ISlideBusiness slideBusiness)
        {
            _slideBusiness = slideBusiness;
        }



        // GET: api/<SlidesController>
        /// <summary>
        ///     Gets a list of all slides. Only available for Administrators.
        /// </summary> 
        /// <remarks>
        /// Sample request: api/Slide
        /// </remarks>
        /// <returns>A List with all slide</returns>
        /// <response code="200">A List with all Slide available</response>
        /// <response code="403">If a non administrator user tries to execute the endpoint.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SlideDTO>))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return Ok(_slideBusiness.GetAll());
        }

        // GET api/<SlidesController>/5
        /// <summary>
        ///     Gets a single Slide based on the ID. Only available for Administrators.
        /// </summary>
        /// <param name="id">Slide ID</param>
        /// <remarks>
        /// Sample request: api/Slide/1
        /// </remarks>
        /// <returns>Slide information.</returns>
        /// <response code="200">Slide Information.</response>
        /// <response code="403">If a non administrator user tries to execute the endpoint.</response>
        /// <response code="404">If the role does not exist.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Slide))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var slide = await _slideBusiness.GetById(id);
            if (slide is null)
            {
                return NotFound("Slide with given id does not exist");
            }

            return Ok(slide.ToSlideResponseDTO());
        }

        /// <summary>
        ///     adds a new Slide to the database. Only available for Administrators.
        /// </summary>
        /// <param name="createRequest"></param>
        /// <remarks>
        /// Sample request: api/slide
        /// </remarks>
        /// <returns>Created Slide.</returns>
        /// <response code="201">Created slide Information.</response>
        /// <response code="403">If a non administrator user tries to execute the endpoint.</response>
        /// <response code="400">If the request lack some field.</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SlideCreateDTO))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize]        
        public async Task<IActionResult> Create([FromForm] SlideCreateDTO createRequest)
        {
            var slide = await _slideBusiness.Create(createRequest);

            return Created("", slide);
        }

        // PUT api/<SlidesController>/5
        /// <summary>
        ///     Update a slide. Only available for Administrators.
        /// </summary>
        /// <param name="id"></param>
        /// /// <param name="dto"></param>
        /// <remarks>
        /// Sample request: api/slide/1
        /// </remarks>
        /// <returns>Updated slide information.</returns>
        /// <response code="200">Updated slide Information.</response>
        /// <response code="403">If a non administrator user tries to execute the endpoint.</response>
        /// <response code="404">If the slide to update was not found.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleDTO))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromForm] SlideCreateDTO dto)
        {
            return await _slideBusiness.Update(id, dto) ? Ok():NotFound() ;
        }

        // DELETE api/<SlidesController>/5
        /// <summary>
        ///     Delete method which deletes an existing slide. Only available for Administrators.
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>
        /// Sample request: api/slide/1
        /// </remarks>
        /// <returns>a true or false if the slide was or not delete.</returns>
        /// <response code="200">if it was delete.</response>
        /// <response code="403">If a non administrator user tries to execute the endpoint.</response>
        /// <response code="404">If the slide to delete was not found.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        [Authorize]
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