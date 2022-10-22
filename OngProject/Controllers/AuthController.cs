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
using Microsoft.AspNetCore.Http;
using OngProject.Entities;

namespace OngProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthBusiness _authBusiness;
        private readonly IUserBusiness _userBusiness;

        public AuthController(IAuthBusiness authBusiness, IUserBusiness userBusiness)
        {
            _authBusiness = authBusiness;
            _userBusiness = userBusiness;
        }

        /// <summary>
        /// Obtain Token for Users LogIn
        /// </summary>
        /// <param name="login">Email and Password</param>
        /// <returns>The token for the user or Admin for valid credentials</returns>
        /// <remarks>
        /// Sample Request:     All Parameters required     
        ///                 /api/Auth/Login
        ///                 {
        ///                     "email": "User Email",
        ///                     "password": "Password"
        ///                 }
        /// </remarks>
        /// <response code="200">Token for private Endpoints when valid credentials</response>
        /// /// <response code="401">If the users data is incorrect or doesn't exist</response>
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(String))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO login)
        {
            string token = await _authBusiness.Login(login);

            if (!string.IsNullOrEmpty(token))
                return Ok(token);

            return Unauthorized("{ok:false}");
        }


        /// <summary>
        /// Register a new User
        /// </summary>
        /// <param name="register">User Information</param>
        /// <returns>The token for the user or Admin for valid credentials</returns>
        /// <remarks>
        /// Sample Request:      All Parameters required           
        ///                 /api/Auth/Register
        ///                 {
        ///                     "firstName": "User Name",
        ///                     "lastName": "User LastName",
        ///                     "email": "user@example.com",
        ///                     "password": "Password"
        ///                 }
        /// </remarks>
        /// <response code="201">Created and Token for private Endpoints</response>
        /// /// <response code="400">If the Register users data is incorrect or a required field is missing</response>
        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(String))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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


        /// <summary>
        /// Obtain my User information
        /// </summary>
        /// <returns>Obtain all private user information</returns>
        /// <remarks>
        /// Sample Request:     Required User to be login       
        ///                 /api/Auth/Me
        /// </remarks>
        /// <response code="200">Private User information</response>
        /// /// <response code="404">If the users token is incorrect or doesn't exist</response>
        [HttpGet("Me")]
        [Authorize(Roles = "User, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserGetDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Me()
        {
            var claim = HttpContext.User.Identity as ClaimsIdentity;
            if (claim != null)
            {
                var id = Int32.Parse(claim.FindFirst("Identifier").Value);

                var user = await _userBusiness.GetById(id);
                return Ok(user);
            }

            return NotFound();

        }
    }
}