using Microsoft.AspNetCore.Mvc;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using OngProject.Core.Interfaces;

namespace OngProject.Controllers
{
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
        public Task<ActionResult<User>> PostTestimonial(User user)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/Testimonial/5
        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteTestimonial(int id)
        {
            throw new NotImplementedException();
        }
    }
}
