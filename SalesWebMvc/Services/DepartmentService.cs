using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _context;

        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> FindAllAsync() // Requisição assincrona
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
       /* public List<Department> FindAll() //requisição sincrona
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }*/