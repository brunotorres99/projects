﻿using DX_Web_Challenge.Core.Criteria;
using DX_Web_Challenge.Core.Models;
using System.Threading.Tasks;

namespace DX_Web_Challenge.Business
{
    public interface IGroupService
    {
        Task<SearchResult<Group>> GetGroups(GroupCriteria criteria);
        Task<Group> GeGroup(int id);
        Task<Group> AddGroup(Group group);
        Task UpdateGroup(int id, Group group);
        Task DeleteGroup(int id);
    }
}