using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OngProject.Core.Business;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User, Admin")]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityBusiness _activityBusiness;
        public ActivityController(IActivityBusiness activityBusiness)
        {
            _activityBusiness = activityBusiness;
        }

        // GET: api/Activity
        [HttpGet]
        public Task<IEnumerable<Activity>> GetActivities()
        {
            throw new NotImplementedException();
        }

        // GET: api/Activity/5
        [HttpGet("{id}")]
        public Task<ActionResult<Activity>> GetActivity(int id)
        {
            throw new NotImplementedException();
        }

        // PUT: api/Activity/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public Task<IActionResult> PutActivity(int id, Activity activity)
        {
            throw new NotImplementedException();
        }

        // POST: api/Activity
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostActivity([FromForm] ActivityDTO activityDto, [Required]IFormFile imageFile)
        {
            activityDto.ImageFile = imageFile.OpenReadStream();

            await _activityBusiness.Add(activityDto);

            return Created(" ", activityDto);

        }

        // DELETE: api/Activity/5
        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteActivity(int id)
        {
            throw new NotImplementedException();
        }
    }
}

