using Teste.Sales.Domain.Entities;
using Teste.Sales.Domain.Interfaces;
using Teste.Sales.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Teste.Sales.Infrastructure.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly SalesDbContext _context;

        public SaleRepository(SalesDbContext context)
        {
            _context = context;
        }

        public void Add(Sale sale)
        {
            _context.Sales.Add(sale);
        }

        public Sale? GetById(Guid id)
        {
            return _context.Sales
                .Include(s => s.Items)
                .FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<Sale> GetAll()
        {
            return _context.Sales
                .Include(s => s.Items)
                .ToList();
        }

        public void Update(Sale sale)
        {
            _context.Sales.Update(sale);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}