using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Teste.Sale.Ambev.Domain.Entities;
using Teste.Sale.Ambev.Infrastructure.Data;
using Teste.Sale.Ambev.Infrastructure.Repositories;

namespace Teste.Sale.Ambev.Unit.Infrastructure.Repositories
{
    public class SaleEfRepositoryTests
    {
        private readonly SalesDbContext _context;
        private readonly SaleEfRepository _repository;

        public SaleEfRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<SalesDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new SalesDbContext(options);
            _repository = new SaleEfRepository(_context);
        }

        [Fact]
        public void GetItemById_ReturnsCorrectItem()
        {
            // Arrange
            var sale = new SaleEntity
            {
                CustomerId = "1",
                CustomerName = "Giovane Teste",
                BranchId = "1",
                BranchName = "Empresa Teste",
                Date = DateTime.UtcNow,
                Items = new List<SaleItemEntity>()
            };

            var itemId = Guid.NewGuid();

            var item = new SaleItemEntity
            {
                SaleItemId = itemId,
                SaleId = sale.SaleId, 
                ProductId = "1",
                ProductName = "Coca Colae",
                Quantity = 5,
                UnitPrice = 6,
                Discount = 0,
                Cancelled = false
            };

            sale.Items.Add(item);

            _repository.Save(sale);

            var result = _repository.GetItemById(itemId);

            result.Should().NotBeNull();
            result.SaleItemId.Should().Be(itemId);
        }
    }
}
