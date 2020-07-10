using System.Threading.Tasks;

namespace DX_Web_Challenge.Data.Repository
{
    public interface IBaseRepository
    {
        Task SaveChangesAsync();
    }
}