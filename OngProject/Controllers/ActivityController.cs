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

        /// <summary>
        ///    Get a single Activity based on the ID.
        /// </summary>
        /// <param name="id">Activity ID</param>
        /// <remarks>
        /// Sample request: api/Activity/1
        /// </remarks>
        /// <returns>The Activity information.</returns>
        /// <response code="200">The whole Activity information.</response>
        /// <response code="404">If the Activity does not exist.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Activity))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivity(int id)
        {
            var activityId = await _activityBusiness.GetById(id);

            if (activityId is null) { return NotFound("Activity does not exist"); } else { return Ok(activityId); }; 
        }

        /// <summary>
        /// Post method which adds a new  Activity to the database.
        /// </summary>
        /// 
        /// /// <param name="activityDto"></param>
        /// /// <param name="imageFile"></param>
        /// <returns>Create new Activity</returns>
        /// <remarks>
        /// Sample Request:
        ///     POST /Activity/
        ///     {      
        ///        "Name": "Name activity.",
        ///        "Content": "Description content.",
        ///        "imageFile": "image of the Activity you want to save."
        ///     }
        /// </remarks>
        /// <response code="200"> Activity was created successfully.</response>
        /// /// <response code="404">Error, the Activity was not created.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActivityDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostActivity([FromForm] ActivityDTO activityDto, [Required] IFormFile imageFile)
        {
            activityDto.ImageFile = imageFile.OpenReadStream();

            await _activityBusiness.Add(activityDto);

            return Created(" ", activityDto);

        }


        /// <summary>
        /// Updates an existing Activity. Only available for Administrators.
        /// </summary>
        /// <param name="id"></param>
        /// /// <param name="activityUpdateDto"></param>
        /// /// <param name="imageFile"></param>
        /// <returns>The Updated Activity</returns>
        /// <remarks>
        /// Sample Request: 
        ///     PUT /Activity/1
        ///     {
        ///        "ID" : "Activity ID to update. Example: 1.",
        ///        "Name": "Name activity.",
        ///        "Content": "Description content.",
        ///        "imageFile": "image of the Activity you want to save."
        ///     }
        /// </remarks>
        /// <response code="200"> Activity was successfully updated.</response>
        /// /// <response code="401">If the user is not an administrator try to run the endpoint.</response>
        /// /// <response code="400"> Activity does not exist</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActivityUpdateDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutActivity(int id, [FromForm] ActivityUpdateDTO activityUpdateDto, IFormFile imageFile)
        {
            var existActivity = await _activityBusiness.DoesExist(id);

            if (existActivity)
            {
                if (imageFile != null) { activityUpdateDto.ImageFile = imageFile.OpenReadStream(); } else { activityUpdateDto.ImageFile = null; }

                var update = await _activityBusiness.Update(id, activityUpdateDto);

                return Ok(update);
            }

            return NotFound("activity does not exist to update");
        }

        // DELETE: api/Activity/5
        //    [HttpDelete("{id}")]
        //    public Task<bool> DeleteActivity(int id)
        //    {
        //        throw new NotImplementedException();
        //    }
    }
}


