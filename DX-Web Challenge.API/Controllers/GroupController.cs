using DX_Web_Challenge.Business.Interfaces;
using DX_Web_Challenge.Core;
using DX_Web_Challenge.Core.Criteria;
using DX_Web_Challenge.Core.DTO;
using DX_Web_Challenge.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace DX_Web_Challenge.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        public async Task<ActionResult<SearchResult<GroupDTO>>> GetGroups([FromQuery] GroupCriteria criteria)
        {
            var result = await _groupService.GetGroups(criteria);

            return new SearchResult<GroupDTO>
            {
                Records = result.Records.Select(x => new GroupDTO(x)),
                RecordCount = result.RecordCount
            };
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GroupDTO>> GeGroups(int id)
        {
            var group = await _groupService.GeGroup(id);

            if (group == null)
            {
                return NotFound();
            }

            return new GroupDTO(group);
        }

        [HttpPost]
        public async Task<ActionResult<GroupDTO>> PostGroups(Group group)
        {
            var response = await _groupService.AddGroup(group);

            return Ok(new ResponseObject<GroupDTO>
            {
                BusinessMessages = response.BusinessMessages,
                Value = new GroupDTO(response.Value)
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GroupDTO>> PutGroups(int id, Group group)
        {
            if (id != group.Id)
            {
                return BadRequest();
            }

            var response = await _groupService.UpdateGroup(id, group);

            return Ok(new ResponseObject<GroupDTO>
            {
                BusinessMessages = response.BusinessMessages,
                Value = new GroupDTO(response.Value)
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GroupDTO>> DeleteGroups(int id)
        {
            var response = await _groupService.DeleteGroup(id);

            return Ok(new ResponseObject<GroupDTO>
            {
                BusinessMessages = response.BusinessMessages,
                Value = new GroupDTO(response.Value)
            });
        }
    }
}