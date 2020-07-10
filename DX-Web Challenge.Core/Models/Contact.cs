using System.Collections.Generic;

namespace DX_Web_Challenge.Core.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public List<string> Telephones { get; set; }

        public ICollection<ContactGroup> ContactGroups { get; set; }
    }
}
