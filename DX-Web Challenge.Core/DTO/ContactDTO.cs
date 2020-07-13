using DX_Web_Challenge.Core.Models;
using System.Collections.Generic;

namespace DX_Web_Challenge.Core.DTO
{
    public class ContactDTO
    {
        public ContactDTO() { }

        public ContactDTO(Contact contact)
        {
            Id = contact.Id;
            FirstName = contact.FirstName;
            LastName = contact.LastName;
            Address = contact.Address;
            Email = contact.Email;
            Photo = contact.Photo;
            Telephones = contact.Telephones;
            RowVersion = contact.RowVersion;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public byte[] Photo { get; set; }
        public List<string> Telephones { get; set; }

        public byte[] RowVersion { get; set; }

        public Contact MapToContact()
        {
            return new Contact
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Address = Address,
                Email = Email,
                Photo = Photo,
                Telephones = Telephones,
                RowVersion = RowVersion
            };
        }
    }
}
