using StudioHair.Core.Interfaces;
using StudioHair.Infrascruture.Context;

namespace StudioHair.Infrascruture.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _context;

        public HomeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
