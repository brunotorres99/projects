using DX_Web_Challenge.Core.Criteria;
using DX_Web_Challenge.Core.Models;
using System.Threading.Tasks;

namespace DX_Web_Challenge.Business.Interfaces
{
    public interface IGroupRepository : IBaseRepository
    {
        Task<Group> FindByIdAsync(int id);
        Task<SearchResult<Group>> GetGroups(GroupCriteria criteria);

        Task AddAsync(Group group);
        public void Remove(Group group);
        public void Update(Group group);
    }
}
