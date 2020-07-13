using System.Collections.Generic;

namespace DX_Web_Challenge.Core.Models
{
    public class Contact : IConcurrencyEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public byte[] Photo { get; set; }
        public string Telephones { get; set; }

        public byte[] RowVersion { get; set; }

        public virtual ICollection<ContactGroup> ContactGroups { get; set; }
    }
}
