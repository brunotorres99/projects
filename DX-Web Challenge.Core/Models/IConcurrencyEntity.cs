namespace DX_Web_Challenge.Core.Models
{
    public interface IConcurrencyEntity
    {
        byte[] RowVersion { get; set; }
    }
}
