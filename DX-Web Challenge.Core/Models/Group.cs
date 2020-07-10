using System.Collections.Generic;

namespace DX_Web_Challenge.Core.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ContactGroup> ContactGroups { get; set; }
    }
}
