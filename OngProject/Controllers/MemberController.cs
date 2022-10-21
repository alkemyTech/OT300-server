using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
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


        /// <summary>
        ///     Gets a paged list of  members. Only available for Administrators.
        /// </summary>
        /// <param name="paginationParams">Page number</param>
        /// <remarks>
        /// Sample request:GET api/member?pageNumber=3&amp;pageSize=10
        /// </remarks>
        /// <returns>A List with the members in the current page.</returns>
        /// <response code="200">The members in the current page. Including the link to the previous and next page.</response>
        /// <response code="400">If page number does not exist, or values are invalid</response>
        /// <response code="401">Not logged in user tries to execute the endpoint</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<MembersDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

        /// <summary>
        ///     Gets a single member based on the ID. Only available for Administrators.
        /// </summary>
        /// <param name="id">Member's ID</param>
        /// <remarks>
        /// Sample request: api/Member/1
        /// </remarks>
        /// <returns>The member's information.</returns>
        /// <response code="200">The whole member's information.</response>
        /// <response code="403">If a non administrator user tries to execute the endpoint.</response>
        /// <response code="401">Not logged in user tries to execute the endpoint.</response>
        /// <response code="404">If the member does not exist.</response>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MembersDTO))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]        
        public async Task<IActionResult> GetById(int id)
        {
            var members = await _memberBusiness.GetById(id);
            return Ok(members.ToPublicDTO());
            //TODO needs refactor sinces is returning Entity from business
            //  return  members !=null ? Ok(members):NotFound(id);
        }




        /// <summary>
        ///   Adds a member to the database. Only available for Administrators.
        /// </summary>
        /// <param name="memberDTO"/>
        /// <param name="imageFile"/>
        /// <remarks>
        /// Sample request:POST api/Member/
        /// </remarks>
        /// <returns>A 201 status code with the member's information.</returns>
        /// <response code="201">The data of the created member.</response>
        /// <response code="401">If a not logged  user tries to execute the endpoint.</response>
        /// <response code="400">If a required field is missing</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MembersDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize]
        //        [SwaggerRequestExample(typeof(MembersDTO), typeof(MembersDTOExample))]
        public async Task<IActionResult> Add([FromForm] MembersDTO memberDTO, IFormFile imageFile)
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



        /// <summary>
        ///    Updates an existing member. Only available for Administrators.
        /// </summary>
        /// <param name="id">Member's ID to update.</param>
        /// <param name="memberUpdateDto"/>
        /// <param name="imageFile"/>
        /// <remarks>
        /// Sample request: api/Member/1
        /// </remarks>
        /// <returns>A 200 status code.</returns>
        /// <response code="200">The member was updated</response>
        /// <response code="401">If a non logged user tries to execute the endpoint.</response>        
        /// <response code="404">Can be due because  the member  doesn't exist.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MemberUpdateDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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




        /// <summary>
        ///     Deletes an existing member. Only available for Administrators.
        /// </summary>
        /// <param name="id">Member's ID to delete.</param>
        /// <remarks>
        /// Sample request: DELETE api/Member/1
        /// </remarks>
        /// <returns>A 200 status code when success.</returns>
        /// <response code="200">A message indicating that the member was deleted succesfully</response>
        /// <response code="401">If a non logged user tries to execute the endpoint.</response>
        /// <response code="404">If the member doesn't exist in the database.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    //TODO :
    //Implement on all controllers , inherit from ActionResult
    //Contructor receives a PagedList

    public class PagedResult<T> where T : class
    {

        public string Next { get; }
        public string Prev { get; }
        public int TotalPages { get; }
        public int CurrentPage { get; }
        public PagedList<T> Data { get; }

        public PagedResult(PagedList<T> data)
        {
            Data = data;
        }
    }

}