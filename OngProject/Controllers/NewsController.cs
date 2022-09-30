using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Entities;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User, Admin")]
    public class NewsController : ControllerBase
    {
        private readonly INewsBusiness _newsService;

        public NewsController(INewsBusiness service)
        {
            _newsService = service;
        }

        [HttpGet("/api/news")]
        public IActionResult GetAllNews()
        {
            var news = _newsService.GetAll();
            return Ok(news);
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
        public async Task<IActionResult> InsertNews([FromBody] News news)
        {
            if (news == null)
            {
                throw new ArgumentNullException(nameof(news));
            }
            else
            {
                await _newsService.Add(news);
                return Ok(news);
            }

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

        //i was looking about this because i don't know if the service return false what it going to happens here.

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganization(int id)
        {
            await _newsService.Delete(id);
            return Ok();
        }


    }
}
