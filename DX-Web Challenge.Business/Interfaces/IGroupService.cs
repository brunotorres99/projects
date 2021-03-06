﻿using DX_Web_Challenge.Core.Criteria;
using DX_Web_Challenge.Core;
using DX_Web_Challenge.Core.Models;
using System.Threading.Tasks;

namespace DX_Web_Challenge.Business.Interfaces
{
    public interface IGroupService
    {
        Task<SearchResult<Group>> GetGroups(GroupCriteria criteria);
        Task<Group> GeGroup(int id);
        Task<ResponseObject<Group>> AddGroup(Group group);
        Task<ResponseObject<Group>> UpdateGroup(int id, Group group);
        Task<ResponseObject<Group>> DeleteGroup(int id);
    }
}