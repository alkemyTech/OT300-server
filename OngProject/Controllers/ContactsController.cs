using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User, Admin")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsBusiness _contactsBusiness;
        public ContactsController(IContactsBusiness business)
        {
            _contactsBusiness = business;
        }

        // GET: api/<ContactsController>
        /// <summary>
        ///    Get all Contacts. Only available for Administrators.
        /// </summary>
        /// <param></param>
        /// <remarks>
        /// Sample request: api/Contacts
        /// </remarks>
        /// <returns>All Contacts</returns>
        /// <response code="200">A list of Contacts</response>
        /// <response code="401">when an no admin user try to use the endpoint</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContactDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetContacts()
        {
            return Ok(_contactsBusiness.GetAllContacts());
        }


        // POST api/<ContactsController>
        /// <summary>
        ///    Create new Contact
        /// </summary>
        /// <param name="values"></param>
        /// <remarks>
        /// Sample request: api/Contacts
        /// </remarks>
        /// <returns>The information of contact created</returns>
        /// <response code="201">Create and return the information of contact</response>
        /// <response code="400">when the information have some missing field</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ContactDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add([FromBody] ContactDTO values)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("data error");
            }

            await _contactsBusiness.AddContact(values);

            return Created("https://localhost:5001/Contacts/", values);
        }

    }
}
