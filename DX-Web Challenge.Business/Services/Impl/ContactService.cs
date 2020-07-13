using DX_Web_Challenge.Core;
using DX_Web_Challenge.Core.Criteria;
using DX_Web_Challenge.Core.Models;
using DX_Web_Challenge.Data.Repository;
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

        public async Task<ResponseObject<Contact>> AddContact(Contact contact)
        {
            var response = new ResponseObject<Contact>();
            await Validate();
            if (response.IsValid == false) return response;

            await _contactRepository.AddAsync(contact);
            await _contactRepository.SaveChangesAsync();

            response.Value = contact;
            return response;

            async Task Validate()
            {
                if (string.IsNullOrWhiteSpace(contact.FirstName))
                {
                    response.AddMessage("FirstName", "The FirstName is required", BusinessMessage.TypeEnum.error);
                }

                if (string.IsNullOrWhiteSpace(contact.LastName))
                {
                    response.AddMessage("LastName", "The LastName is required", BusinessMessage.TypeEnum.error);
                }
            }
        }

        public async Task<ResponseObject<Contact>> UpdateContact(int id, Contact contact)
        {
            var response = new ResponseObject<Contact>();
            await Validate();
            if (response.IsValid == false) return response;

            _contactRepository.Update(contact);
            await _contactRepository.SaveChangesAsync();

            response.Value = contact;
            return response;

            async Task Validate()
            {
                var savedContact = await _contactRepository.FindByIdAsync(id);
                if (savedContact == null)
                {
                    response.AddMessage("Contact", "Contact not Found", BusinessMessage.TypeEnum.error);
                    return;
                }

                if (savedContact.RowVersion != contact.RowVersion)
                {
                    response.AddMessage("Contact", "This Contact is already updated", BusinessMessage.TypeEnum.error);
                }

                if (string.IsNullOrWhiteSpace(contact.FirstName))
                {
                    response.AddMessage("FirstName", "The FirstName is required", BusinessMessage.TypeEnum.error);
                }

                if (string.IsNullOrWhiteSpace(contact.LastName))
                {
                    response.AddMessage("LastName", "The LastName is required", BusinessMessage.TypeEnum.error);
                }
            }
        }

        public async Task<ResponseObject<Contact>> DeleteContact(int id)
        {
            var response = new ResponseObject<Contact>();
            await Validate();
            if (response.IsValid == false) return response;

            var contact = await _contactRepository.FindByIdAsync(id);
            _contactRepository.Remove(contact);
            await _contactRepository.SaveChangesAsync();

            response.Value = contact;
            return response;

            async Task Validate()
            {
                var savedContact = await _contactRepository.FindByIdAsync(id);
                if (savedContact == null)
                {
                    response.AddMessage("Contatc", "Contact not Found", BusinessMessage.TypeEnum.error);
                    return;
                }
            }
        }
    }
}