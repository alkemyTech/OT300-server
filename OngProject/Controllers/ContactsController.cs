﻿using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        [HttpGet("Contacts")]
        public IActionResult GetContacts()
        {
            return Ok(_contactsBusiness.GetAllContacts());
        }
        [Authorize]

        // GET api/<ContactsController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ContactsController>
        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Post([FromBody] ContactDTO contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("data error");
            }

            await _contactsBusiness.AddContact(contact);

            return Ok(contact);
        }

    }
}
