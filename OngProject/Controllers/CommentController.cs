using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Entities;
using OngProject.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User, Admin")]
    public class CommentController : Controller
    {
        private readonly ICommentBusiness _commentBusiness;
        private readonly IUserBusiness _userBusiness;
        private readonly INewsBusiness _newsBusiness;

        public CommentController(ICommentBusiness commentBusiness, IUserBusiness userBusiness,INewsBusiness newsBusiness)
        {
            _commentBusiness = commentBusiness;
            _userBusiness = userBusiness;
            _newsBusiness = newsBusiness;
        }
                
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetListComment()
        {
            return Ok(_commentBusiness.GetAll().OrderBy(x =>x.CreatedAt));
        }


        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateCommentDTO content)
        {

            var doesExist = await _commentBusiness.DoesExist(id);

            if (!doesExist)
            {
                return NotFound("Comment not found");
            }

            var claim = HttpContext.User.Identity as ClaimsIdentity;
            var userid = Int32.Parse(claim.FindFirst("Identifier").Value);
            var commentDto = await _commentBusiness.Update(content.Body, id, userid);

            if (commentDto is null)
            {
                return Forbid("Bearer");
            }

            return Ok(commentDto);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateComment([FromBody] CommentAddDto commentAddDto)
        {
            var comment = await _commentBusiness.Add(commentAddDto);
            if (comment != null)
            {
                return Ok();
            }
            return BadRequest();
        }


        [HttpDelete("{id}")]
        [Authorize]
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
                else
                {
                    return BadRequest("user don't have permission");
                }

            }
            return BadRequest("The user must have Login");
        }



    }
}