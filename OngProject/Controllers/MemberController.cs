using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
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
        public async Task<IActionResult> Add(Member members)
        {
            await _memberBusiness.Add(members);
            return Ok();
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put( Member members)
        {
            var member = await _memberBusiness.Update(members);

            return Ok(member);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var member = await _memberBusiness.Delete(id);
            return Ok(member);
        }
    }
}
