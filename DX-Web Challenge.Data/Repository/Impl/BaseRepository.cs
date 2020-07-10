using System.Threading.Tasks;

namespace DX_Web_Challenge.Data.Repository.Impl
{
    public class BaseRepository : IBaseRepository
    {
        protected readonly ContactContext _context;

        public BaseRepository(ContactContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
