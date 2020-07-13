using DX_Web_Challenge.Business.Interfaces;
using DX_Web_Challenge.Core;
using DX_Web_Challenge.Core.Criteria;
using DX_Web_Challenge.Core.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace DX_Web_Challenge.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<ActionResult<SearchResult<ContactDTO>>> GetContacts([FromQuery]ContactCriteria criteria)
        {
            var result = await _contactService.GetContacts(criteria);

            return new SearchResult<ContactDTO>
            {
                Records = result.Records.Select(x => new ContactDTO(x)),
                RecordCount = result.RecordCount
            };
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDTO>> GeContacts(int id)
        {
            var contact = await _contactService.GeContact(id);

            if (contact == null)
            {
                return NotFound();
            }

            return new ContactDTO(contact);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseObject<ContactDTO>>> PostContacts(ContactDTO contact)
        {
            var response = await _contactService.AddContact(contact.MapToContact());

            return Ok(new ResponseObject<ContactDTO>
            {
                BusinessMessages = response.BusinessMessages,
                Value = new ContactDTO(response.Value)
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseObject<ContactDTO>>> PutContacts(int id, ContactDTO contact)
        {
            if (id != contact.Id)
            {
                return BadRequest();
            }

            var response = await _contactService.UpdateContact(id, contact.MapToContact());

            return Ok(new ResponseObject<ContactDTO>
            { 
                BusinessMessages = response.BusinessMessages,
                Value = new ContactDTO(response.Value)
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseObject<ContactDTO>>> DeleteContacts(int id)
        {
            var response = await _contactService.DeleteContact(id);

            return Ok(new ResponseObject<ContactDTO>
            {
                BusinessMessages = response.BusinessMessages,
                Value = new ContactDTO(response.Value)
            });
        }
    }
}