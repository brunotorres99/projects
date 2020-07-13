﻿using DX_Web_Challenge.Business;
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
            await _groupService.AddGroup(group);

            return CreatedAtAction("GetGroups", new { id = group.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroups(int id, Group group)
        {
            if (id != group.Id)
            {
                return BadRequest();
            }

            await _groupService.UpdateGroup(id, group);

            return CreatedAtAction("GetGroups", new { id = id });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGroups(int id)
        {
            await _groupService.DeleteGroup(id);

            return Ok();
        }
    }
}