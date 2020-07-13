using System.Threading.Tasks;

namespace DX_Web_Challenge.Business.Interfaces
{
    public interface IBaseRepository
    {
        Task SaveChangesAsync();
    }
}