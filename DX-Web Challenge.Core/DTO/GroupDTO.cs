using DX_Web_Challenge.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace DX_Web_Challenge.Core.DTO
{
    public class GroupDTO
    {
        public GroupDTO() { }

        public GroupDTO(Group group)
        {
            if (group == null) return;

            Id = group.Id;
            Name = group.Name;
            RowVersion = group.RowVersion;
            ContactGroups = group.ContactGroups?.Select(x => new ContactGroupDTO
            {
                Id = x.Id,
                ContactId = x.ContactId,
                GroupId = x.GroupId
            }).ToList();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ContactGroupDTO> ContactGroups { get; set; }

        public byte[] RowVersion { get; set; }

        public Group MapToGroup()
        {
            return new Group
            {
                Id = Id,
                Name = Name,
                RowVersion = RowVersion,
                ContactGroups = ContactGroups?.Select(x => new ContactGroup
                {
                    Id = x.Id,
                    ContactId = x.ContactId,
                    GroupId = x.GroupId
                }).ToList()
            };
        }
    }
}