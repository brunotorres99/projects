using DX_Web_Challenge.Core.Criteria;
using DX_Web_Challenge.Core.Models;
using DX_Web_Challenge.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DX_Web_Challenge.Business.Services.Impl
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<SearchResult<Contact>> GetContacts(ContactCriteria criteria)
        {
            return await _contactRepository.GetContacts(criteria);
        }

        public async Task<Contact> GeContact(int id)
        {
            return await _contactRepository.FindByIdAsync(id);
        }

        public async Task<Contact> AddContact(Contact contact)
        {
            await _contactRepository.AddAsync(contact);
            await _contactRepository.SaveChangesAsync();

            return contact;
        }

        public async Task UpdateContact(int id, Contact contact)
        {
            _contactRepository.Update(contact);

            try
            {
                await _contactRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // todo
            }
        }

        public async Task DeleteContact(int id)
        {
            var contact = await _contactRepository.FindByIdAsync(id);
            if (contact == null)
            {
                // todo
            }

            _contactRepository.Remove(contact);
            await _contactRepository.SaveChangesAsync();
        }
    }
}