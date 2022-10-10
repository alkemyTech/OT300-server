﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
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
        public IActionResult GetAll()
        {
            var members = _memberBusiness.GetAll();
            return Ok(members);
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
        [Authorize(Roles = "User")]
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