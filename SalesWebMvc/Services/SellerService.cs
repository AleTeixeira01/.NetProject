using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using SalesWebMvc.Data;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        /*public List<Seller> FindAll() //Requisição sincrona
        {
            return _context.Seller.ToList();
        }*/

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        /*public void Insert(Seller obj) //Requisição sincrona
        {
            _context.Add(obj);
            _context.SaveChanges();
        }*/

        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        /* public Seller FindById(int id) //Requisição sincrona
         {
             return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
         }*/

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        /*public void Remove(int id)//Requisição sincrona
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }*/

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException("Can't delete seller because he/she has sales");
            }
        }

        /* public void Update(Seller obj)//Requisição sincrona
         {
             if (!_context.Seller.Any(x => x.Id == obj.Id))
             {
                 throw new NotFoundException("Id not found");
             }
             try
             {
                 _context.Update(obj);
                 _context.SaveChanges();
             }
             catch (DbUpdateConcurrencyException e)
             {
                 throw new DbConcurrencyException(e.Message);
             }
         }*/

        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}

