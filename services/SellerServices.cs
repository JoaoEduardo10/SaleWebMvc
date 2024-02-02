using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.services.Exceptions;

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

        public Seller? FindById(int id)
        {
            return _context.Seller.Include(seller => seller.Department).FirstOrDefault(seller => seller.Id == id);
        }

        public void Remove(int id)
        {
            var seller = FindById(id);

            if (seller != null)
            {
                _context.Seller.Remove(seller);
                _context.SaveChanges();
            }

        }

        public void Update(Seller obj)
        {
            var isSeller = _context.Seller.Any(seller => seller.Id == obj.Id);

            if (!isSeller)
            {
                throw new NotFoundException("Id não encontrado!");
            }

            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (DbConcurrencyException error)
            {
                throw new DbConcurrencyException(error.Message);
            }
        }
    }
}