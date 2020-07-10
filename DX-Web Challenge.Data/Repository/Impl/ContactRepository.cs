using DX_Web_Challenge.Core;
using DX_Web_Challenge.Core.Criteria;
using DX_Web_Challenge.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DX_Web_Challenge.Data.Repository.Impl
{
    public class ContactRepository : BaseRepository, IContactRepository
    {
        public ContactRepository(ContactContext context) : base(context)
        { }

        public async Task<Contact> FindByIdAsync(int id)
        {
            return await _context.Contacts.FindAsync(id);
        }

        public async Task<SearchResult<Contact>> GetContacts(ContactCriteria criteria)
        {
            var query = _context.Contacts.AsQueryable();

            if(string.IsNullOrWhiteSpace(criteria?.SearchQuery) == false)
            {
                query = query.Where(x => x.Name.Contains(criteria.SearchQuery));
            }

            var count = await query.CountAsync();

            if (string.IsNullOrWhiteSpace(criteria?.SortField))
            {
                query = query.OrderBy(criteria.SortField, criteria.IsAscending);
            }
            else
            {
                query = query.OrderBy(x => x.Name);
            }

            if(criteria?.HasPagination == true)
            {
                query = query.Skip(criteria.SkipValue).Take(criteria.PageSize.Value);
            }

            var records = await query.ToListAsync();

            return new SearchResult<Contact>
            {
                Records = records,
                RecordCount = count
            };
        }

        public async Task AddAsync(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
        }

        public void Remove(Contact contact)
        {
            _context.Contacts.Remove(contact);
        }

        public void Update(Contact contact)
        {
            _context.Contacts.Update(contact);
        }
    }
}
