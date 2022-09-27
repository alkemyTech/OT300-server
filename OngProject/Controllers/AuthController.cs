using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;

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
        public IActionResult Login(UserLoginDTO login)
        {
            string token = _authBusiness.Login(login);

            if (!string.IsNullOrEmpty(token))
                return Ok(token);

            return Unauthorized("{ok:false}");
        }


        [HttpPost("/auth/register")]
        public IActionResult Register([FromBody] RegisterDTO register)
        {
            var result = _authBusiness.Register(register);

            return Ok(result);
        }

    }
}