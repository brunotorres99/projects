using DX_Web_Challenge.Business.Interfaces;
using DX_Web_Challenge.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DX_Web_Challenge.Business.Test
{
    public class GroupTests : BaseTest
    {
        private IGroupService _groupService;
        private IContactService _contactService;

        public GroupTests() : base()
        {
            _groupService = ServiceProvider.GetService<IGroupService>();
            _contactService = ServiceProvider.GetService<IContactService>();
        }

        [Test]
        public async Task AddGroupTest()
        {
            var contact = new Contact
            {
                FirstName = "Test FirstName",
                LastName = "Test LastName"
            };

            await _contactService.AddContact(contact);

            var group = new Group
            {
                Name = "Test Group",
                ContactGroups = new List<ContactGroup>
                {
                    new ContactGroup
                    {
                        ContactId = 1
                    }
                }
            };

            await _groupService.AddGroup(group);

            Assert.IsTrue(group.Id > 0);
        }

        [Test]
        public async Task GetGroupTest()
        {
            var group = new Group
            {
                Name = "Test Group"
            };

            await _groupService.AddGroup(group);

            var GroupGet = await _groupService.GeGroup(1);

            Assert.IsTrue(GroupGet.Id > 0);
        }

        [Test]
        public async Task UpdateGroupTest()
        {
            var contactC1 = new Contact
            {
                FirstName = "Test FirstName",
                LastName = "Test LastName"
            };

            await _contactService.AddContact(contactC1);

            var group = new Group
            {
                Name = "Test Group",
                ContactGroups = new List<ContactGroup>
                {
                    new ContactGroup
                    {
                        ContactId = 1
                    }
                }
            };

            await _groupService.AddGroup(group);

            var contactC2 = new Contact
            {
                FirstName = "Test FirstName",
                LastName = "Test LastName"
            };

            await _contactService.AddContact(contactC2);

            var groupBeforeUpdate = await _groupService.GeGroup(1);

            groupBeforeUpdate.ContactGroups.First().ContactId = 2;

            var response = await _groupService.UpdateGroup(1, groupBeforeUpdate);

            var contactAfterUpdate = await _groupService.GeGroup(1);

            Assert.IsTrue(contactAfterUpdate.ContactGroups.First().ContactId == 2);
        }
    }
}