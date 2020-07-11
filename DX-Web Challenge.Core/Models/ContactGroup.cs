namespace DX_Web_Challenge.Core.Models
{
    public class ContactGroup
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public int GroupId { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual Group Group { get; set; }
    }
}
