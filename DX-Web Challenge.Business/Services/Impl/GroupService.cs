using DX_Web_Challenge.Core.Criteria;
using DX_Web_Challenge.Core.DTO;
using DX_Web_Challenge.Core.Models;
using DX_Web_Challenge.Data.Repository;
using System.Threading.Tasks;

namespace DX_Web_Challenge.Business.Services.Impl
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        public GroupService(IGroupRepository GroupRepository)
        {
            _groupRepository = GroupRepository;
        }

        public async Task<SearchResult<Group>> GetGroups(GroupCriteria criteria)
        {
            return await _groupRepository.GetGroups(criteria);
        }

        public async Task<Group> GeGroup(int id)
        {
            return await _groupRepository.FindByIdAsync(id);
        }

        public async Task<ResponseObject<Group>> AddGroup(Group group)
        {
            var response = new ResponseObject<Group>();
            await Validate();
            if (response.IsValid == false) return response;

            await _groupRepository.AddAsync(group);
            await _groupRepository.SaveChangesAsync();

            response.Value = group;
            return response;

            async Task Validate()
            {
                if (string.IsNullOrWhiteSpace(group.Name))
                {
                    response.AddMessage("Name", "The Name is required", BusinessMessage.TypeEnum.error);
                }
            }
        }

        public async Task<ResponseObject<Group>> UpdateGroup(int id, Group group)
        {
            var response = new ResponseObject<Group>();
            await Validate();
            if (response.IsValid == false) return response;

            _groupRepository.Update(group);

            await _groupRepository.SaveChangesAsync();

            response.Value = group;
            return response;

            async Task Validate()
            {
                var savedGroup = await _groupRepository.FindByIdAsync(id);
                if (savedGroup == null)
                {
                    response.AddMessage("Group", "Group not Found", BusinessMessage.TypeEnum.error);
                    return;
                }

                if (savedGroup.RowVersion != group.RowVersion)
                {
                    response.AddMessage("Group", "This Group is already updated", BusinessMessage.TypeEnum.error);
                }

                if (string.IsNullOrWhiteSpace(group.Name))
                {
                    response.AddMessage("Name", "The Name is required", BusinessMessage.TypeEnum.error);
                }
            }
        }

        public async Task<ResponseObject<Group>> DeleteGroup(int id)
        {
            var response = new ResponseObject<Group>();
            await Validate();
            if (response.IsValid == false) return response;

            var group = await _groupRepository.FindByIdAsync(id);
            _groupRepository.Remove(group);
            await _groupRepository.SaveChangesAsync();

            response.Value = group;
            return response;

            async Task Validate()
            {
                var savedGroup = await _groupRepository.FindByIdAsync(id);
                if (savedGroup == null)
                {
                    response.AddMessage("Group", "Group not Found", BusinessMessage.TypeEnum.error);
                    return;
                }
            }
        }
    }
}