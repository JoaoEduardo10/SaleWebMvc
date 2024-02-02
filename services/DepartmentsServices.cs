using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.services
{
    public class DepartmentsServices
    {
        private readonly SalesWebMvcContext _context;


        public DepartmentsServices(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(list => list.Name).ToListAsync();
        }
    }
}