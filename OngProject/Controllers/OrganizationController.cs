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
    [Authorize(Roles ="Admin")]

    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationBusiness _organizationService;
        private readonly ISlideBusiness _slideBusiness;

        public OrganizationController(IOrganizationBusiness service, ISlideBusiness slideBusiness)
        {
            _organizationService = service;
            _slideBusiness = slideBusiness;
        }

        /// <summary>
        ///     Get All public information of Organization and all the Slides in order
        /// </summary> 
        /// <remarks>
        /// Sample request: api/organization/public
        /// </remarks>
        /// <returns>All public information</returns>
        /// <response code="200">All public information</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrganizationPublicDTO))]
        [HttpGet("public")]
        [AllowAnonymous]
        public IActionResult GetOrganizationPublicInfo()
        {
            var orgPubInfoDTO = _organizationService.GetPublicInfo();
            var slidesOrganizations = _slideBusiness.GetAllSlides();
            return Ok(new { orgPubInfoDTO, slidesOrganizations });
        }


        /// <summary>
        ///     Update Organization information. Only available for Administrators.
        /// </summary>
        /// <param name="updateDTO"></param>
        /// /// <param name="Image"></param>
        /// <remarks>
        /// Sample request: api/organization/public
        /// </remarks>
        /// <returns>Updated Organization Information</returns>
        /// <response code="200">Updated Information</response>
        /// <response code="401">If the user was not log in</response>
        /// <response code="403">If an user that is not Admin try to update</response>
        /// <response code="404">Organization not found</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrganizationPublicDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("public")]
        [Authorize] 
        public async Task<IActionResult> UpdateOrganizationPublicInfo([FromForm] OrganizationPostPublicDTO updateDTO,[Required] IFormFile Image)
        {
            updateDTO.ImageStream = Image.OpenReadStream();
            var updated = await _organizationService.Update(updateDTO);
            return updated is not null ? Ok(new { updated }) : NotFound();
        }

        /* Comment out for further advance in the structure
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganization(int id)
        {
            await _organizationService.Delete(id);
            return Ok();
        }
        */

    }
}
