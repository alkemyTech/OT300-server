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
            if (paginationParams.PageNumber > pageMembers.TotalPages)
            {
                return BadRequest($"page number {paginationParams.PageNumber} doesn't exist");
            }
            else
            {
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

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromForm] MemberUpdateDTO memberUpdateDto, IFormFile imageFile)
        {
            var existActivity = await _memberBusiness.DoesExist(id);

            if (existActivity)
            {
                if (imageFile != null) { memberUpdateDto.ImageFile = imageFile.OpenReadStream(); } else { memberUpdateDto.ImageFile = null; }

                await _memberBusiness.Update(id, memberUpdateDto);

                return Ok("The member was updated successfully");
            }

            return NotFound("Member does not exist to update");
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<bool>> DeleteMember(int id)
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