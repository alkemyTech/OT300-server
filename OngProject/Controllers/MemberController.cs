using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User, Admin")]
    public class MemberController : ControllerBase
    {
        private readonly IMemberBusiness _memberBusiness;
        public MemberController(IMemberBusiness memberBusiness)
        {
            _memberBusiness = memberBusiness;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAll([FromQuery] PaginationParams paginationParams)
        {

            PagedList<MembersDTO> pageMembers = _memberBusiness.GetAll(paginationParams);
            var url = this.Request.Path;
            return Ok(new
            {
                next = pageMembers.HasNext ? $"{url}/{paginationParams.PageNumber + 1}" : "",
                prev = (pageMembers.Count > 0 && pageMembers.HasPrevious) ? $"{url}/{paginationParams.PageNumber - 1}" : "",
                totalPages = pageMembers.TotalPages,
                currentPage = pageMembers.CurrentPage,
                data = pageMembers
            });
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {

            var members =  await _memberBusiness.GetById(id);

            return Ok(members);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add([FromForm]MembersDTO memberDTO, IFormFile imageFile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                memberDTO.Image = imageFile.OpenReadStream();

                await _memberBusiness.Add(memberDTO);
                return Ok(memberDTO);
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put( Member members)
        {
            var member = await _memberBusiness.Update(members);

            return Ok(member);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<bool>> DeleteTestimonial(int id)
        {

            var doesExist = await _memberBusiness.DoesExist(id);
            if (!doesExist)
            {
                return NotFound();
            }

            //Here member will never be null since we check above so we can just delete
            await _memberBusiness.Delete(id);

            return Ok();

        }
    }
}