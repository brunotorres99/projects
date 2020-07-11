using DX_Web_Challenge.Core;
using DX_Web_Challenge.Core.Criteria;
using DX_Web_Challenge.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DX_Web_Challenge.Data.Repository.Impl
{
    public class GroupRepository : BaseRepository, IGroupRepository
    {
        public GroupRepository(ContactContext context) : base(context)
        { }

        public async Task<Group> FindByIdAsync(int id)
        {
            return await _context.Groups.FindAsync(id);
        }

        public async Task<SearchResult<Group>> GetGroups(GroupCriteria criteria)
        {
            var query = _context.Groups.AsQueryable();

            if (string.IsNullOrWhiteSpace(criteria?.SearchQuery) == false)
            {
                query = query.Where(x => EF.Functions.Like(x.Name, $"%{criteria.SearchQuery}%"));
            }

            var count = await query.CountAsync();

            if (string.IsNullOrWhiteSpace(criteria?.SortField))
            {
                query = query.OrderBy(x => x.Name);
            }
            else
            {
                query = query.OrderBy(criteria.SortField, criteria.IsAscending);
            }

            if (criteria?.HasPagination == true)
            {
                query = query.Skip(criteria.SkipValue).Take(criteria.PageSize.Value);
            }

            var records = await query.ToListAsync();

            return new SearchResult<Group>
            {
                Records = records,
                RecordCount = count
            };
        }

        public async Task AddAsync(Group group)
        {
            await _context.Groups.AddAsync(group);
        }

        public void Remove(Group group)
        {
            _context.Groups.Remove(group);
        }

        public void Update(Group group)
        {
            _context.Groups.Update(group);
        }
    }
}
