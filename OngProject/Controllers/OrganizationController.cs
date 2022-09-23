using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Entities;
using System.Linq;
using System.Threading.Tasks;
using System;
using OngProject.Core.Interfaces;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationBusiness _organizationService;

        public OrganizationsController(IOrganizationBusiness service)
        {
            _organizationService = service;
        }
    
        [HttpGet("/api/organization")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllOrganizations()
        {
            var organizations = _organizationService.GetAll();
            return Ok(organizations);
        }

        [HttpGet("/api/organization/public")]
        [AllowAnonymous]
        public IActionResult GetOrganizationPublicInfo()
        {
            var orgPubInfoDTO= _organizationService.GetPublicInfo();
            return Ok(orgPubInfoDTO);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByIdOrganization(int id)
        {
            var organization = await _organizationService.GetById(id);
            return Ok(organization);
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrganization([FromBody] Organization organization)
        {
            if (organization == null)
            {
                throw new ArgumentNullException(nameof(organization));
            }
            else
            {
                await _organizationService.Insert(organization);
                return Ok(organization);
            }
                       
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrganization(int id, [FromQuery] Organization Organization)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
                }
                else
                {
                    var result = await _organizationService.Update(id, Organization);
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
            await _organizationService.Delete(id);
            return Ok();
        }


    }
}
