using DX_Web_Challenge.Business;
using DX_Web_Challenge.Core.Criteria;
using DX_Web_Challenge.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DX_Web_Challenge.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _GroupService;

        public GroupController(IGroupService GroupService)
        {
            _GroupService = GroupService;
        }

        [HttpGet]
        public async Task<ActionResult<SearchResult<Group>>> GetGroups([FromQuery] GroupCriteria criteria)
        {
            return await _GroupService.GetGroups(criteria);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> GeGroups(int id)
        {
            var group = await _GroupService.GeGroup(id);

            if (group == null)
            {
                return NotFound();
            }

            return group;
        }

        [HttpPost]
        public async Task<ActionResult<Group>> PostGroups(Group group)
        {
            await _GroupService.AddGroup(group);

            return CreatedAtAction("GetGroups", new { id = group.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroups(int id, Group group)
        {
            if (id != group.Id)
            {
                return BadRequest();
            }

            await _GroupService.UpdateGroup(id, group);

            return CreatedAtAction("GetGroups", new { id = id });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGroups(int id)
        {
            await _GroupService.DeleteGroup(id);

            return Ok();
        }
    }
}