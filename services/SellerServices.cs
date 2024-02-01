using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.services
{
    public class SellerServices
    {
        private readonly SalesWebMvcContext _context;


        public SellerServices(SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj)

        {
            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}