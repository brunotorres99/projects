using DX_Web_Challenge.Business;
using DX_Web_Challenge.Core.Criteria;
using DX_Web_Challenge.Core.Models;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<SearchResult<Contact>>> GetContacts(ContactCriteria criteria)
        {
            return await _contactService.GetContacts(criteria);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GeContacts(int id)
        {
            var contact = await _contactService.GeContact(id);

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }

        [HttpPost]
        public async Task<ActionResult<Contact>> PostContacts(Contact contact)
        {
            await _contactService.AddContact(contact);

            return CreatedAtAction("GetContacts", new { id = contact.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutContacts(int id, Contact contact)
        {
            if (id != contact.Id)
            {
                return BadRequest();
            }

            await _contactService.UpdateContact(id, contact);

            return CreatedAtAction("GetContacts", new { id = id });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteContacts(int id)
        {
            await _contactService.DeleteContact(id);

            return Ok();
        }
    }
}