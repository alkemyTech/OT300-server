using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Services;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public UsersController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Users2
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            var users = _unitOfWork.Users.GetAll();
            return users;
        }

        // GET: api/Users2/5
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _unitOfWork.Users.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users2/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Users.Update(user);

            try
            {
                _unitOfWork.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users2
        [HttpPost]
        public ActionResult<User> PostUser(User user)
        {
            _unitOfWork.Users.Insert(user);
            _unitOfWork.SaveChanges();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users2/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _unitOfWork.Users.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            _unitOfWork.Users.Delete(user);
            _unitOfWork.SaveChanges();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _unitOfWork.Users.GetById(id) != null;
        }
    }
}

