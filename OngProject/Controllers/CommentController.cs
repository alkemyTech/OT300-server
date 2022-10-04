using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Entities;
using OngProject.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ICommentBusiness _commentBusiness;
        private readonly IUserBusiness _userBusiness;

        public CommentController(ICommentBusiness commentBusiness, IUserBusiness userBusiness)
        {
            _commentBusiness = commentBusiness;
            _userBusiness = userBusiness;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetListComment()
        {
            return Ok(_commentBusiness.GetAll());
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "User, Admin")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var claim = HttpContext.User.Identity as ClaimsIdentity;

            if (claim != null)
            {
                var userId = Int32.Parse(claim.FindFirst("Identifier").Value);

                var comment = _commentBusiness.GetById(id);

                if (comment.Id != userId) 
                {
                    return Ok(await _commentBusiness.Delete(id));
                }
                else{
                    return BadRequest("user don't have permission");
                } 
                
            } 
            return BadRequest("The user must have Login");
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CommentAddDto commentAddDto)
        {
            var comment = await _commentBusiness.Add(commentAddDto);
            if (comment != null)
            {
                return Ok();
            }
            return BadRequest();
        }


    }
}
