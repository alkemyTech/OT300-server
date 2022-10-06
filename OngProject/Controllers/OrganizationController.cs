using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationBusiness _organizationService;
        private readonly ISlideBusiness _slideBusiness;

        public OrganizationController(IOrganizationBusiness service, ISlideBusiness slideBusiness)
        {
            _organizationService = service;
            _slideBusiness = slideBusiness;
        }

        [HttpGet("/api/organization/public")]
        [AllowAnonymous]
        public IActionResult GetOrganizationPublicInfo()
        {
            var orgPubInfoDTO = _organizationService.GetPublicInfo();
            var slidesOrganizations = _slideBusiness.GetAllSlides();
            return Ok(new { orgPubInfoDTO, slidesOrganizations });
        }

        [HttpPost("/api/organization/public")]
        [Authorize(Roles ="Admin")] 
        public async Task<IActionResult> UpdateOrganizationPublicInfo([FromForm] OrganizationPostPublicDTO updateDTO,[Required] IFormFile Image)
        {
            updateDTO.ImageStream = Image.OpenReadStream();
            var updated = await _organizationService.Update(updateDTO);
            return updated is not null ? Ok(new { updated }) : NotFound();
        }

        /****** 
        [HttpGet("/api/organization")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllOrganizations()
        {
            var organizations = _organizationService.GetAll();
            return Ok(organizations);
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

        //i was looking about this because i don't know if the service return false what it going to happens here.

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganization(int id)
        {
            await _organizationService.Delete(id);
            return Ok();
        }
        *////

    }
}
