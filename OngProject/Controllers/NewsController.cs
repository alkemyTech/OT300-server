﻿using Microsoft.AspNetCore.Authorization;
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
        private readonly ICategoryBusiness _categoryBusiness;

        public NewsController(INewsBusiness service,ICommentBusiness commentBusiness, ICategoryBusiness categoryBusiness)
        {
            _newsService = service;
            _commentBusiness = commentBusiness;
            _categoryBusiness = categoryBusiness;
        }


        [HttpGet]
        [Authorize]
        public IActionResult GetAll([FromQuery] int? page = 1)
        {
            PagedList<NewsDTO> pageNews = _newsService.GetAllPage(page.Value);

            if(page > pageNews.TotalPages)
            {
                return BadRequest($"page number {page} doesn't exist");
            }
            else
            {
                var url = this.Request.Path;
                return Ok(new
                {
                    data = pageNews,
                    next = pageNews.HasNext ? $"{url}/{page + 1}" : "",
                    prev = (pageNews.Count > 0 && pageNews.HasPrevious) ? $"{url}/{page - 1}" : "",
                    currentPage = pageNews.CurrentPage,
                    totalPages = pageNews.TotalPages,
                });
            }
            
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

        [HttpGet("{id}/comment")]
        [Authorize]
        public async Task<IActionResult> ListCommentByNew(int id)
        {
            var existNew = await _newsService.DoesExist(id);
            if (existNew)
            {
                return Ok(_commentBusiness.showListCommentDto(id));
            }
            else { return BadRequest($"News with id{id} doesn't exist"); }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateNews(int id, [FromForm] NewsPutDTO newsDTO, IFormFile imageFile)
        {
            var catExist = await _categoryBusiness.DoesExist(newsDTO.IdCategory);

            if (catExist)
            {
                var exist = await _newsService.DoesExist(id);

                if (!exist)
                {
                    return NotFound("the News you want to update doesn't exist");
                }
                else
                {
                    if (newsDTO.ImageFile != null) { newsDTO.ImageFile = imageFile.OpenReadStream(); } else { newsDTO.ImageFile = null; }
                    var result = await _newsService.Update(id, newsDTO);
                    return Ok(result);
                }
            }
            else
            {
                return NotFound("Category doesn't exist");
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

    }
}