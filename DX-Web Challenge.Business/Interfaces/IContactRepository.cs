using DX_Web_Challenge.Core.Criteria;
using DX_Web_Challenge.Core.Models;
using System.Threading.Tasks;

namespace DX_Web_Challenge.Business.Interfaces
{
    public interface IContactRepository : IBaseRepository
    {
        Task<Contact> FindByIdAsync(int id);
        Task<SearchResult<Contact>> GetContacts(ContactCriteria criteria);

        Task AddAsync(Contact contact);
        public void Remove(Contact contact);
        public void Update(Contact contact);
    }
}