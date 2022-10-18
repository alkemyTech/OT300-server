using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OngProject.Core.Business;
using OngProject.Core.Models.DTOs;
using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User, Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;
        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }


        /// <summary>
        ///    List of all registered users.  Only available for Administrators.
        /// </summary>
        /// <remarks>
        /// Sample request: api/User/
        /// </remarks>
        /// <returns>List of registered users.</returns>
        /// /// <response code="200"> Shows the list of Users.</response>
        /// /// <response code="401">If the user is not an administrator try to run the endpoint.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserGetDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUsers()
        {
            return Ok(_userBusiness.GetAllUsers());
        }


        /// <summary>
        /// Update an existing User.
        /// </summary>
        /// <param name="id"></param>
        /// /// <param name="user"></param>
        /// <returns>The Updated User</returns>
        /// <remarks>
        /// Sample Request: 
        ///     PUT /User/1
        ///     {
        ///        "ID" : "ID User. Example: 1.",
        ///        "FirstName": "Name user.",
        ///        "LastName": "LastName user.",
        ///        "Email": "example@example.com",
        ///        "Password": "abcdefgh" -- "12345678",
        ///        "Photo": "Picture of the user you want to save."
        ///     }
        /// </remarks>
        /// <response code="200"> User was successfully updated.</response>
        /// /// <response code="404"> User does not exist</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserPatchDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("{id}")]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserPatchDTO user)
        {
            int userId = int.Parse((HttpContext.User.Identity as ClaimsIdentity).FindFirst("Identifier").Value);
            string role = (HttpContext.User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.Role).Value;

            if (userId != id && role != "Admin")
                return Forbid();

            UserPatchDTO updatedUserProfile = await _userBusiness.Update(id, user);
            return updatedUserProfile is not null ? Ok(updatedUserProfile) : NotFound();
        }

        /// <summary>
        /// Delete an existing User.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Empty response</returns>
        /// <remarks>
        /// Sample Request:
        ///     Delete /User/2
        /// </remarks>
        /// <response code="200">If User Was deleted</response>
        /// /// <response code="400">If User does not exist</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var claim = HttpContext.User.Identity as ClaimsIdentity;

            if (claim != null)
            {
                var userId = Int32.Parse(claim.FindFirst("Identifier").Value); 
                
                var userDelete = await _userBusiness.GetById(id);
               
                if (userDelete != null && userDelete.Id == userId)
                {
                    await _userBusiness.Delete(id);
                    return Ok();
                }

                return BadRequest("user don't have permission");

            }

            return BadRequest("User must Login");

        }
    }
}

