﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OngProject.Core.Business;
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

        // GET: api/Users
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult GetUsers()
        {
            return Ok(_userBusiness.GetAllUsers());
        }

        // GET: api/Users2/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public Task<ActionResult<User>> GetUser(int id)
        {
            throw new NotImplementedException();
        }

        // PUT: api/Users2/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public Task<IActionResult> PutUser(int id, User user)
        {
            throw new NotImplementedException();
        }

        // POST: api/Users2
        [HttpPost]
        public Task<ActionResult<User>> PostUser(User user)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/Users2/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var claim = HttpContext.User.Identity as ClaimsIdentity;

            if (claim != null)
            {
                var userId = Int32.Parse(claim.FindFirst("Identifier").Value); 
                
                var userDelete = await _userBusiness.GetById(id);
                
                if (userDelete.Id == userId)
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

