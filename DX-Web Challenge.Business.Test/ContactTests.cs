using DX_Web_Challenge.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Text;
using System.Threading.Tasks;

namespace DX_Web_Challenge.Business.Test
{
    public class ContactTests : BaseTest
    {
        private IContactService _contactService;

        public ContactTests() : base()
        {
            _contactService = ServiceProvider.GetService<IContactService>();
        }

        [Test]
        public async Task AddContactTest()
        {
            var contact = new Contact
            {
                FirstName = "Test FirstName",
                LastName = "Test LastName",
                Photo = Encoding.ASCII.GetBytes("data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDIBKE0DHxgljNBAAO9TXL0Y4OHwAAAABJRU5ErkJggg==")
            };

            await _contactService.AddContact(contact);

            Assert.IsTrue(contact.Id > 0);
        }

        [Test]
        public async Task GetContactTest()
        {
            var contact = new Contact
            {
                FirstName = "Test FirstName",
                LastName = "Test LastName"
            };

            await _contactService.AddContact(contact);

            var contactGet = await _contactService.GeContact(1);

            Assert.IsTrue(contactGet.Id > 0);
        }

        [Test]
        public async Task UpdateContactTest()
        {
            var contact = new Contact
            {
                FirstName = "Test FirstName",
                LastName = "Test LastName"
            };

            await _contactService.AddContact(contact);

            var contactBeforeUpdate = await _contactService.GeContact(1);
            contactBeforeUpdate.Email = "Test Email";

            await _contactService.UpdateContact(1, contactBeforeUpdate);

            var contactAfterUpdate = await _contactService.GeContact(1);

            Assert.IsTrue(contactAfterUpdate.Email == "Test Email");
        }

        [Test]
        public async Task UpdateContactTestConcurrencyFail()
        {
            var contact = new Contact
            {
                FirstName = "Test FirstName",
                LastName = "Test LastName"
            };

            await _contactService.AddContact(contact);

            var contactBeforeUpdate = new Contact
            {
                FirstName = "Test FirstName",
                LastName = "Test LastName"
            };

            contactBeforeUpdate.Email = "Test Email";
            contactBeforeUpdate.RowVersion = Guid.NewGuid().ToByteArray(); // to fail

            await _contactService.UpdateContact(1, contactBeforeUpdate);

            var contactAfterUpdate = await _contactService.GeContact(1);

            Assert.IsTrue(contactAfterUpdate.Email == "Test Email");

            // todo not working
        }
    }
}