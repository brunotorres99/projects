using DX_Web_Challenge.Core.Criteria;
using DX_Web_Challenge.Core;
using DX_Web_Challenge.Core.Models;
using System.Threading.Tasks;

namespace DX_Web_Challenge.Business
{
    public interface IContactService
    {
        Task<SearchResult<Contact>> GetContacts(ContactCriteria criteria);
        Task<Contact> GeContact(int id);
        Task<ResponseObject<Contact>> AddContact(Contact contact);
        Task<ResponseObject<Contact>> UpdateContact(int id, Contact contact);
        Task<ResponseObject<Contact>> DeleteContact(int id);
    }
}