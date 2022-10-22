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
using Microsoft.AspNetCore.Http;

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

        /// <summary>
        ///    Get all comments orders by time of creation. Only available for Administrators.
        /// </summary>
        /// <param></param>
        /// <remarks>
        /// Sample request: api/Comment
        /// </remarks>
        /// <returns>All Comments in order</returns>
        /// <response code="200">All Comments in order</response>
        /// <response code="403">when an no admin user try to use the endpoint</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommentDTO))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetListComment()
        {
            return Ok(_commentBusiness.GetAll().OrderBy(x =>x.CreatedAt));
        }

        /// <summary>
        ///    Update an existing comment.
        /// </summary>
        /// <param name="content"></param>
        /// <remarks>
        /// Sample request: api/Comment/1
        /// </remarks>
        /// <returns>comment updated information</returns>
        /// <response code="200">comment updated</response>
        /// <response code="403">when an user that is not the owner of the comment try to update</response>
        /// <response code="404">When the comment is not found</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommentDTO))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
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

        /// <summary>
        ///    adds a new Comment to the database.
        /// </summary>
        /// <param name="commentAddDto"></param>
        /// <remarks>
        /// Sample request: api/Comments
        /// </remarks>
        /// <returns>comment information</returns>
        /// <response code="201">comment created</response>
        /// <response code="400">when the request lack a field</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Comment))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateComment([FromBody] CommentAddDto commentAddDto)
        {
            var comment = await _commentBusiness.Add(commentAddDto);
            if (comment != null)
            {
                return Created("",comment);
            }
            return BadRequest();
        }

        /// <summary>
        ///    Delete of an existing comment.
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>
        /// Sample request: api/Comment/1
        /// </remarks>
        /// <returns>a true or false if the comment is deleted</returns>
        /// <response code="200">comment Deleted</response>
        /// <response code="403">when an user that is not the owner of the comment try to delete</response>
        /// <response code="404">When the comment is not found</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]        
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var claim = HttpContext.User.Identity as ClaimsIdentity;

            var userId = Int32.Parse(claim.FindFirst("Identifier").Value);

            var comment = _commentBusiness.GetById(id);

            if (comment is null)
            {
                return NotFound("Comment does not exist");
            }
            else if(comment.Id != userId)
            {
                return Ok(await _commentBusiness.Delete(id));
            }
            else
            {
                return Forbid("user don't have permission");
            }           
            
        }



    }
}