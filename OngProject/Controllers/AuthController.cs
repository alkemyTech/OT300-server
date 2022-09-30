using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Templates;

namespace OngProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthBusiness _authBusiness;

        public AuthController(IAuthBusiness authBusiness)
        {
            _authBusiness = authBusiness;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserLoginDTO login)
        {
            string token = _authBusiness.Login(login);

            if (!string.IsNullOrEmpty(token))
                return Ok(token);

            return Unauthorized("{ok:false}");
        }


        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterDTO register)
        {
            //Todo: Throws Exception whe same email is used. It should not try to save to db if email is being used..
            var result = _authBusiness.Register(register);

            //Todo: Should we return the user or the token? if token is not returning why not just return the result?
            return Ok(new { newUser = result, /*token = token*/});;
        }
    }
}