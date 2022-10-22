using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using Microsoft.AspNetCore.Authorization;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleBusiness _roleBusiness;

        public RoleController(IRoleBusiness roleBusiness)
        {
            _roleBusiness = roleBusiness;
        }

        /// <summary>
        ///     Gets a list of all Roles. Only available for Administrators.
        /// </summary> 
        /// <remarks>
        /// Sample request: api/Role
        /// </remarks>
        /// <returns>A List with all Roles available</returns>
        /// <response code="200">A List with all Roles available</response>
        /// <response code="403">If a non administrator user tries to execute the endpoint.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RoleDTO>))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var roles = _roleBusiness.GetAll();
            return Ok(roles);
        }

        /// <summary>
        ///     Gets a single Role based on the ID. Only available for Administrators.
        /// </summary>
        /// <param name="id">Role ID</param>
        /// <remarks>
        /// Sample request: api/Role/1
        /// </remarks>
        /// <returns>Role information.</returns>
        /// <response code="200">Role Information.</response>
        /// <response code="403">If a non administrator user tries to execute the endpoint.</response>
        /// <response code="404">If the role does not exist.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleDTO))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var role = _roleBusiness.GetById(id);
            if (role is null)
            {
                return NotFound($"Role with id: {id} does not exist");
            }

            return Ok(role);
        }

        /// <summary>
        ///     adds a new Role to the database. Only available for Administrators.
        /// </summary>
        /// <param name="request"></param>
        /// <remarks>
        /// Sample request: api/Role
        /// </remarks>
        /// <returns>Created role information.</returns>
        /// <response code="201">Create role Information.</response>
        /// <response code="403">If a non administrator user tries to execute the endpoint.</response>
        /// <response code="400">If the request lack some field.</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RoleDTO))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public IActionResult Create(RoleDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Manual mapping until we get a mapper
            var role = new Role()
            {
                Name = request.Name,
                Description = request.Description
            };
            var result = _roleBusiness.Create(role);
            return Created("", result);
        }

        /// <summary>
        ///     Update a Role. Only available for Administrators.
        /// </summary>
        /// <param name="id"></param>
        /// /// <param name="request"></param>
        /// <remarks>
        /// Sample request: api/Role/1
        /// </remarks>
        /// <returns>Updated role information.</returns>
        /// <response code="200">Updated role Information.</response>
        /// <response code="403">If a non administrator user tries to execute the endpoint.</response>
        /// <response code="400">If the request lack some field.</response>
        /// <response code="404">If the role to update was not found.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleDTO))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RoleDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roleToUpdate = await _roleBusiness.GetById(id);
            if (roleToUpdate is null)
            {
                return NotFound($"Role with id: {id} does not exist");
            }

            //Manual mapping until we get a mapper
            roleToUpdate.Name = request.Name;
            roleToUpdate.Description = request.Description;

            var result = _roleBusiness.Update(roleToUpdate);
            return Ok(result);
        }

        /// <summary>
        ///     Delete method which deletes an existing Role. Only available for Administrators.
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>
        /// Sample request: api/Role/1
        /// </remarks>
        /// <returns>a true or false if the role was or not delete.</returns>
        /// <response code="200">if it was delete.</response>
        /// <response code="403">If a non administrator user tries to execute the endpoint.</response>
        /// <response code="404">If the role to delete was not found.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var doesExist = await _roleBusiness.DoesExist(id);
            if (!doesExist)
            {
                return NotFound($"Role with id: {id} does not exist");
            }

            await _roleBusiness.Delete(id);
            return Ok();
        }
    }
}