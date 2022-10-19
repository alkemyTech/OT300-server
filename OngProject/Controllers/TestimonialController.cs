using Microsoft.AspNetCore.Mvc;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using OngProject.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using OngProject.Core.Business;
using OngProject.Core.Models.DTOs;
using OngProject.Repositories;
using OngProject.Core.Models;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, User")]
    public class TestimonialController : Controller
    {
        private readonly ITestimonialBusiness _testimonialBusiness;
        public TestimonialController(ITestimonialBusiness testimonialBusiness)
        {
            _testimonialBusiness = testimonialBusiness;
        }

        // GET: api/Testimonial
        /// <summary>
        ///    GetAll Testimonial.
        /// </summary>
        /// <param name="page">Testimonial </param>
        /// <remarks>
        /// Sample request: api/Testimonial?page=1
        /// </remarks>
        /// <returns>A list of Testimonial  .</returns>
        /// <response code="200">A Testimonial list.</response>
        /// <response code="400">If Testimonial page does not exist.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TestimonialListDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        [Authorize]
        public IActionResult GetAllTestimonials([FromQuery] int page=1)
        {
            PagedList<TestimonialListDTO> pageTestimony = _testimonialBusiness.GetAllPaged(page);

            if(page > pageTestimony.TotalPages)
            {
                return BadRequest($"page number {page} doesn't exist");
            }
            else
            {
                var url = this.Request.Path;
                return Ok(new
                {
                    next = pageTestimony.HasNext ? $"{url}/{page + 1}" : "",
                    prev = (pageTestimony.Count > 0 && pageTestimony.HasPrevious) ? $"{url}/{page - 1}" : "",
                    totalPages = pageTestimony.TotalPages,
                    currentPage = pageTestimony.CurrentPage,
                    data = pageTestimony
                });
            }
        }

        // GET: api/Testimonial/5
        //[HttpGet("{id}")]
        //[Authorize]
        //public Task<IActionResult> GetTestimonial(int id)
        //{
        //    throw new NotImplementedException();
        //}

        // PUT: api/Testimonial/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754



        /// <summary>
        /// Updates an existing Testimonial. Only available for Administrators.
        /// </summary>
        /// <param name="updateTestimonial"></param>
        /// <returns>The Updated Testimonial</returns>
        /// <remarks>
        /// Sample Request: 
        ///     PUT api/Testimonial/1
        ///     {
        /// 
        ///        "Name": "Name Testimonial.",
        ///        "Content": "Testimonial Content", 
        ///        "imageFile": "image of the Testimonial you want to save."
        ///     }
        /// </remarks>
        /// <response code="200"> Testimonial was successfully updated.</response>
        /// /// <response code="401">If the user is not an administrator try to run the endpoint.</response>
        /// /// <response code="404"> Testimonial does not exist</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TestimonialDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("{id}")]
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> PutTestimonial(int id, [FromForm] TestimonialUpdateDTO updateTestimonial, IFormFile ImageFile)
        {
            var exist = await _testimonialBusiness.DoesExist(id);

            if (exist)
            {
                if (ImageFile != null) { updateTestimonial.Image = ImageFile.OpenReadStream(); } else { updateTestimonial.Image = null; }
                var updated = await _testimonialBusiness.Update(id, updateTestimonial);
                return Ok(updated); 
            }

            return NotFound("The testimonial to update doesn't exist");
        }

        // POST: api/Testimonial
        /// <summary>
        /// Post method which adds a new  Testimonial to the database.
        /// </summary>
        /// 
        /// /// <param name="testimonialDTO"></param>
        /// <returns>Update the Testimonial</returns>
        /// <remarks>
        /// Sample Request:
        ///     POST /Testimonial/
        ///     {      
        ///        "Name": "Name Testimonial.",
        ///        "Content": "Description content.",
        ///        "imageFile": "image of the Testimonial you want to save."
        ///     }
        /// </remarks>
        /// <response code="200"> Testimonial was updated successfully.</response>
        /// /// <response code="400">Error, the Testimonial was not update.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TestimonialDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> PostTestimonial([FromForm] TestimonialDTO testimonialDTO, IFormFile imageFile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                testimonialDTO.Image = imageFile.OpenReadStream();
                await _testimonialBusiness.Add(testimonialDTO);
                return Ok(testimonialDTO);
            }
        }

        // DELETE: api/Testimonial/5
        /// <summary>
		///     Delete method which deletes an existing testimonial. Only available for Administrators.
		/// </summary>
		/// <param name="id">Testimonial ID to delete.</param>
		/// <remarks>
		/// Sample request: api/Testimonial/1
		/// </remarks>
		/// <returns>A 200 status code .</returns>
		/// <response code="200">A message indicating that the testimonial was deleted succesfully</response>
		/// <response code="401">If a non administrator user tries to execute the endpoint.</response>
		/// <response code="404">If the testimonial doesn't exist in the database.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TestimonialDTO))]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> DeleteTestimonial(int id)
        {
            var delete = await _testimonialBusiness.Delete(id);
            
            return delete;
        }
    }
}
