using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.services
{
    public class SalesRecordServices
    {
        private readonly SalesWebMvcContext _context;
        public SalesRecordServices(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? max_date, DateTime? min_date)
        {
            var result = from obj in _context.SalesRecord select obj;

            if (min_date.HasValue)
            {
                result = result.Where(x => x.Date >= min_date.Value);
            }

            if (max_date.HasValue)
            {
                result = result.Where(x => x.Date <= max_date.Value);
            }

            return await result
            .Include(x => x.Seller)
            .Include(x => x.Seller.Department)
            .OrderByDescending(x => x.Date)
            .ToListAsync();
        }
    }
}