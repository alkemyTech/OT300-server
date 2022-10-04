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

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestimonialController : Controller
    {
        private readonly ITestimonialBusiness _testimonialBusiness;
        public TestimonialController(ITestimonialBusiness testimonialBusiness)
        {
            _testimonialBusiness = testimonialBusiness;
        }

        // GET: api/Testimonial
        [HttpGet]
        public Task<IEnumerable<User>> GetAllTestimonials()
        {
            throw new NotImplementedException();
        }

        // GET: api/Testimonial/5
        [HttpGet("{id}")]
        public Task<ActionResult<User>> GetTestimonial(int id)
        {
            throw new NotImplementedException();
        }

        // PUT: api/Testimonial/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public Task<IActionResult> PutTestimonial(int id, User user)
        {
            throw new NotImplementedException();
        }

        // POST: api/Testimonial
        [HttpPost]
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
        [Authorize(Roles = "User, Admin")]
        public async Task<ActionResult<bool>> DeleteTestimonial(int id)
        {
            var delete = await _testimonialBusiness.Delete(id);
            
            return delete;
        }
    }
}
