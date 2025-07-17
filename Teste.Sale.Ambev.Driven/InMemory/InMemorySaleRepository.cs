using Teste.Sale.Ambev.Domain;
using Teste.Sale.Ambev.Domain.Entities;
using Teste.Sale.Ambev.Domain.Repositories;

namespace Teste.Sale.Ambev.Driven.InMemory
{
    public class InMemorySaleRepository : ISaleRepository
    {
        private static readonly List<SaleEntity> Sales = new();

        public SaleEntity? GetById(Guid saleId) => Sales.FirstOrDefault(x => x.SaleId == saleId);

        public IEnumerable<SaleEntity> GetAll() => Sales;

        public void Save(SaleEntity sale)
        {
            Sales.Add(sale);
        }

        public void Update(SaleEntity sale)
        {
            var index = Sales.FindIndex(s => s.SaleId == sale.SaleId);
            if (index >= 0)
            {
                Sales[index] = sale;
            }
        }

        public SaleItemEntity? GetItemById(Guid saleItemId)
        {
            return Sales
                .SelectMany(s => s.Items)
                .FirstOrDefault(i => i.SaleItemId == saleItemId);
        }
    }

}
