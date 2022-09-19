using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;
        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        // GET: api/Users2/5
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            throw new NotImplementedException();
        }

        // PUT: api/Users2/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutUser(int id, User user)
        {
            throw new NotImplementedException();
        }

        // POST: api/Users2
        [HttpPost]
        public ActionResult<User> PostUser(User user)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/Users2/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            throw new NotImplementedException();
        }
    }
}

