using System.Collections.Generic;
using System.Linq;

namespace DX_Web_Challenge.Core.DTO
{
    public class ResponseObject<T> where T : class
    {
        public ResponseObject()
        {
            BusinessMessages = new List<BusinessMessage>();
        }

        public T Value { get; set; }
        public ICollection<BusinessMessage> BusinessMessages { get; set; }

        public void AddMessage(string field, string message, BusinessMessage.TypeEnum type)
        {
            BusinessMessages.Add(new BusinessMessage
            {
                Field = field,
                Message = message,
                Type = type
            });
        }
        public bool IsValid => BusinessMessages?.Any(x => x.Type == BusinessMessage.TypeEnum.error) == false;
    }

    public class BusinessMessage
    {
        public string Field { get; set; }
        public string Message { get; set; }
        public TypeEnum Type { get; set; }

        public enum TypeEnum { success, error, info, warning }
    }
}