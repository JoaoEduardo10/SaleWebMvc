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

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller obj)

        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller?> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(seller => seller.Department).FirstOrDefaultAsync(seller => seller.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            var seller = await FindByIdAsync(id);

            if (seller != null)
            {
                _context.Seller.Remove(seller);
                await _context.SaveChangesAsync();
            }

        }

        public async Task UpdateAsync(Seller obj)
        {
            var isSeller = await _context.Seller.AnyAsync(seller => seller.Id == obj.Id);

            if (!isSeller)
            {
                throw new NotFoundException("Id não encontrado!");
            }

            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbConcurrencyException error)
            {
                throw new DbConcurrencyException(error.Message);
            }
        }
    }
}