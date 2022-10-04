using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Templates;

using System.Security.Claims;
using System;
using System.Linq;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace OngProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthBusiness _authBusiness;
        private readonly IUserBusiness _userBusiness;

        public AuthController(IAuthBusiness authBusiness,IUserBusiness userBusiness)
        {
            _authBusiness = authBusiness;
            _userBusiness = userBusiness;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO login)
        {
            string token = await _authBusiness.Login(login);

            if (!string.IsNullOrEmpty(token))
                return Ok(token);

            return Unauthorized("{ok:false}");
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO register)
        {

            var result = await _authBusiness.Register(register);

            if (result is null)
            {
                return BadRequest("There's an user registered with that email. Please try another one.");
            }

            string token = await _authBusiness.Generate(result);

            return Created("", token);
        }

        [HttpGet("Me")]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> Me()
        {
            var claim = HttpContext.User.Identity as ClaimsIdentity;
            if(claim != null)
            {
                var id = Int32.Parse(claim.FindFirst("Identifier").Value);

                var user = await _userBusiness.GetById(id);
                return Ok(user);
            }
            
            return NotFound();
            
        }
    }
}