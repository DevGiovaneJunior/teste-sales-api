using Microsoft.EntityFrameworkCore;
using Teste.Sale.Ambev.Domain.Entities;
using Teste.Sale.Ambev.Domain.Repositories;
using Teste.Sale.Ambev.Infrastructure.Data;

namespace Teste.Sale.Ambev.Infrastructure.Repositories
{
    public class SaleEfRepository : ISaleRepository
    {
        private readonly SalesDbContext _context;

        public SaleEfRepository(SalesDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SaleEntity> GetAll()
            => _context.Sales.Include(s => s.Items).ToList();

        public SaleEntity? GetById(Guid saleId)
            => _context.Sales.Include(s => s.Items).FirstOrDefault(s => s.SaleId == saleId);

        public SaleItemEntity? GetItemById(Guid saleItemId)
        {
            var sale = _context.Sales
                .Include(s => s.Items)
                .FirstOrDefault(s => s.Items.Any(i => i.SaleItemId == saleItemId));

            return sale?.Items.FirstOrDefault(i => i.SaleItemId == saleItemId);
        }

        public void Save(SaleEntity sale)
        {
            _context.Sales.Add(sale);
            _context.SaveChanges();
        }

        public void Update(SaleEntity sale)
        {
            _context.Sales.Update(sale);
            _context.SaveChanges();
        }
    }
}
