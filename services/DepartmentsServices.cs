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

        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(list => list.Name).ToList();
        }
    }
}