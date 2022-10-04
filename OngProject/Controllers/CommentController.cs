using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ICommentBusiness _commentBusiness;

        public CommentController(ICommentBusiness commentBusiness)
        {
            _commentBusiness = commentBusiness;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetListComment()
        {
            return Ok(_commentBusiness.GetAll());
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
