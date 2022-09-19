using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleBusiness _roleBusiness;

        public RoleController(IRoleBusiness roleBusiness)
        {
            _roleBusiness = roleBusiness;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var roles = _roleBusiness.GetAll();
            return Ok(roles);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var role = _roleBusiness.GetById(id);
            if (role is null)
            {
                return NotFound($"Role with id: {id} does not exist");
            }

            return Ok(role);
        }

        [HttpPost]
        public IActionResult Create(RoleDTO request)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            //Manual mapping until we get a mapper
            var role = new Role()
            {
                Name = request.Name,
                Description = request.Description
            };
            var result = _roleBusiness.Create(role);
            return CreatedAtRoute(nameof(Get), result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, RoleDTO request)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var roleToUpdate = await _roleBusiness.GetById(id);
            if (roleToUpdate is null)
            {
                return NotFound($"Role with id: {id} does not exist");
            }

            //Manual mapping until we get a mapper
            roleToUpdate.Name = request.Name;
            roleToUpdate.Description = request.Description;

            var result = _roleBusiness.Update(roleToUpdate);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var roleToDelete = await _roleBusiness.GetById(id);
            if (roleToDelete is null)
            {
                return NotFound($"Role with id: {id} does not exist");
            }

            await _roleBusiness.Delete(roleToDelete);
            return Ok();
        }
    }
}