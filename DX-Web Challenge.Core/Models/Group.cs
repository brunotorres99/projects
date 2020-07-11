using System.Collections.Generic;

namespace DX_Web_Challenge.Core.Models
{
    public class Group : IConcurrencyEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public byte[] RowVersion { get; set; }

        public virtual ICollection<ContactGroup> ContactGroups { get; set; }
    }
}
