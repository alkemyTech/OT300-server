using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Entities;
using System.Linq;
using System.Threading.Tasks;
using System;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Business;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using OngProject.Repositories;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User, Admin")]
    public class NewsController : ControllerBase
    {
        private readonly INewsBusiness _newsService;
        private readonly ICommentBusiness _commentBusiness;

        public NewsController(INewsBusiness service,ICommentBusiness commentBusiness)
        {
            _newsService = service;
            _commentBusiness = commentBusiness;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult GetAll([FromQuery] int? page = 1)
        {
             PagedList<NewsDTO> pageNews = _newsService.GetAllPage(page.Value);
            var url = this.Request.Path;
            return Ok(new
            {
                data = pageNews,
                next = pageNews.HasNext ? $"{url}/{page + 1}" : "",
                prev = (pageNews.Count > 0 && pageNews.HasPrevious) ? $"{url}/{page - 1}" : ""
            });
        }
 

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByIdNews(int id)
        {
            News news = await _newsService.GetById(id);

            if (news is null)
            {
                return NotFound("New does not exist");
            }

            return Ok(news);
        }

        [HttpPost]       
        public async Task<IActionResult> InsertNews([FromForm] NewsPostDTO dto, [Required]IFormFile imageFile)
        {
            dto.ImageFile =imageFile.OpenReadStream();

            NewsFullDTO created = await _newsService.Add(dto);

            if (created.IdCategory == 0)
                return Conflict($"Category doesn't exists : {dto.IdCategory} ");
            if (created.Id == 0)
                return Conflict($"There was an error");
            return Created("", created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNews(int id, [FromQuery] News news)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
                }
                else
                {
                    var result = await _newsService.Update(news);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var doesExist = await _newsService.DoesExist(id);
            if (doesExist)
            {
                return NotFound();
            }
            await _newsService.Delete(id);

            return Ok("Deleted succesfully");
        }
        [HttpGet("{id}/comment")]
        [Authorize]
        public async Task<IActionResult> ListCommentByNew(int id)
        {
            var existNew = await _newsService.DoesExist(id);
            if (existNew)
            {
                return Ok(_commentBusiness.showListCommentDto(id));
            }
            else { return BadRequest("id not found"); }
        }
    }
}