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
        [HttpGet]
        [Authorize]
        public IActionResult GetAllTestimonials([FromQuery] int page=1)
        {
            PagedList<TestimonialListDTO> pageTestimony = _testimonialBusiness.GetAllPaged(page);
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

        // GET: api/Testimonial/5
        [HttpGet("{id}")]
        [Authorize]
        public Task<IActionResult> GetTestimonial(int id)
        {
            throw new NotImplementedException();
        }

        // PUT: api/Testimonial/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> DeleteTestimonial(int id)
        {
            var delete = await _testimonialBusiness.Delete(id);
            
            return delete;
        }
    }
}
