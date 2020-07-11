using DX_Web_Challenge.Core.Criteria;
using DX_Web_Challenge.Core.Models;
using DX_Web_Challenge.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DX_Web_Challenge.Business.Services.Impl
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _GroupRepository;
        public GroupService(IGroupRepository GroupRepository)
        {
            _GroupRepository = GroupRepository;
        }

        public async Task<SearchResult<Group>> GetGroups(GroupCriteria criteria)
        {
            return await _GroupRepository.GetGroups(criteria);
        }

        public async Task<Group> GeGroup(int id)
        {
            return await _GroupRepository.FindByIdAsync(id);
        }

        public async Task<Group> AddGroup(Group group)
        {
            await _GroupRepository.AddAsync(group);
            await _GroupRepository.SaveChangesAsync();

            return group;
        }

        public async Task UpdateGroup(int id, Group group)
        {
            _GroupRepository.Update(group);

            try
            {
                await _GroupRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw;// todo
            }
        }

        public async Task DeleteGroup(int id)
        {
            var Group = await _GroupRepository.FindByIdAsync(id);
            if (Group == null)
            {
                // todo
            }

            _GroupRepository.Remove(Group);
            await _GroupRepository.SaveChangesAsync();
        }
    }
}