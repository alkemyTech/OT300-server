using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Services.Interfaces;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var roles = _roleService.GetAll();
            return Ok(roles);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var role = _roleService.GetById(id);
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
            var result = _roleService.Create(role);
            return CreatedAtRoute(nameof(Get), result);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, RoleDTO request)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var roleToUpdate = _roleService.GetById(id);
            if (roleToUpdate is null)
            {
                return NotFound($"Role with id: {id} does not exist");
            }

            //Manual mapping until we get a mapper
            roleToUpdate.Name = request.Name;
            roleToUpdate.Description = request.Description;

            var result = _roleService.Update(roleToUpdate);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var roleToDelete = _roleService.GetById(id);
            if (roleToDelete is null)
            {
                return NotFound($"Role with id: {id} does not exist");
            }

            _roleService.Delete(roleToDelete);
            return Ok();
        }
    }
}