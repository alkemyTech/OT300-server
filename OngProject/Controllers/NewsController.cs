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
        private readonly ICategoryBusiness _categoryBusiness;

        public NewsController(INewsBusiness service, ICommentBusiness commentBusiness, ICategoryBusiness categoryBusiness)
        {
            _newsService = service;
            _commentBusiness = commentBusiness;
            _categoryBusiness = categoryBusiness;
        }


        /// <summary>
        ///     Gets a list of maximum 10 news in a page. 
        /// </summary>
        /// <param name="page">Page number</param>
        /// <remarks>
        /// Sample request: api/News?page=3
        /// </remarks>
        /// <returns>A List with the news in the current page.</returns>
        /// <response code="200">The news in the current page. Including the link to the previous and next page.</response>
        /// <response code="400">If page number does not exist</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NewsDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAll([FromQuery] int? page = 1)
        {
            PagedList<NewsDTO> pageNews = _newsService.GetAllPage(page.Value);

            if (page > pageNews.TotalPages)
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

        /// <summary>
        ///     Gets a single new based on the ID. Only available for Administrators.
        /// </summary>
        /// <param name="id">New's ID</param>
        /// <remarks>
        /// Sample request: api/News/1
        /// </remarks>
        /// <returns>The new's information.</returns>
        /// <response code="200">The whole new's information.</response>
        /// <response code="401">If a non administrator user tries to execute the endpoint.</response>
        /// <response code="404">If the new does not exist.</response>
        [HttpGet("{id}")]
		[Authorize(Roles = "Admin")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(News))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdNews(int id)
        {
            News news = await _newsService.GetById(id);

            if (news is null)
            {
                return NotFound("New does not exist");
            }

            return Ok(news);
        }

        /// <summary>
        ///     Gets a list of comments based on the new's ID.
        /// </summary>
        /// <param name="id">New's ID</param>
        /// <remarks>
        /// Sample request: api/News/1/comment/
        /// </remarks>
        /// <returns>A list of the new's comments.</returns>
        /// <response code="200">A list of the new's comments.</response>
        /// <response code="404">If the new does not exist.</response>
        [HttpGet("{id}/comment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommentAddDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListCommentByNew(int id)
        {
            var existNew = await _newsService.DoesExist(id);
            if (existNew)
            {
                return Ok(_commentBusiness.showListCommentDto(id));
            }
            else { return BadRequest($"News with id{id} doesn't exist"); }
        }

        /// <summary>
        ///     Post method which adds a new to the database. Only available for Administrators.
        /// </summary>
        /// <param name="dto"/>
        /// <param name="imageFile"/>
        /// <remarks>
        /// Sample request: api/News/
        /// </remarks>
        /// <returns>A 201 status code with the new's information.</returns>
        /// <response code="201">The data of the created new.</response>
        /// <response code="401">If a non administrator user tries to execute the endpoint.</response>
        /// <response code="409">Can be due because the category doesn't exist or because there was an error adding the new.</response>
        [HttpPost]
		[Authorize(Roles = "Admin")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NewsFullDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> InsertNews([FromForm] NewsPostDTO dto, [Required] IFormFile imageFile)
        {
            dto.ImageFile = imageFile.OpenReadStream();

            NewsFullDTO created = await _newsService.Add(dto);

            if (created.IdCategory == 0)
                return Conflict($"Category doesn't exists : {dto.IdCategory} ");
            if (created.Id == 0)
                return Conflict($"There was an error");
            return Created("", created);
        }

		/// <summary>
		///     Put method which updates an existing new. Only available for Administrators.
		/// </summary>
		/// <param name="id">New's ID to update.</param>
		/// <param name="newsDTO"/>
		/// <param name="imageFile"/>
		/// <remarks>
		/// Sample request: api/News/1
		/// </remarks>
		/// <returns>A 200 status code with the updated information.</returns>
		/// <response code="200">The updated new</response>
		/// <response code="401">If a non administrator user tries to execute the endpoint.</response>
		/// <response code="404">Can be due because either the category doesn't exist nor the new doesn't exist.</response>
		[HttpPut("{id}")]
		[Authorize(Roles = "Admin")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NewsFullDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]

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

		/// <summary>
		///     Put method which updates an existing new. Only available for Administrators.
		/// </summary>
		/// <param name="id">New's ID to delete.</param>
		/// <remarks>
		/// Sample request: api/News/1
		/// </remarks>
		/// <returns>A 200 status code with the updated information.</returns>
		/// <response code="200">A message indicating that the new was deleted succesfully</response>
		/// <response code="401">If a non administrator user tries to execute the endpoint.</response>
		/// <response code="404">If the new doesn't exist in the database.</response>
		[HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NewsFullDTO))]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
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