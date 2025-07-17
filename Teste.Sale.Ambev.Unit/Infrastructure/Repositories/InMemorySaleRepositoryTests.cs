using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Sale.Ambev.Domain.Entities;
using Teste.Sale.Ambev.Driven.InMemory;

namespace Teste.Sale.Ambev.Unit.Infrastructure.Repositories
{
    public class InMemorySaleRepositoryTests
    {
        [Fact]
        public void GetItemById_ReturnsItem_WhenExists()
        {
            var repo = new InMemorySaleRepository();
            var sale = new SaleEntity
            {
                Items = new List<SaleItemEntity>
            {
                new SaleItemEntity { SaleItemId = Guid.NewGuid() }
            }
            };
            repo.Save(sale);

            var item = repo.GetItemById(sale.Items.First().SaleItemId);
            item.Should().NotBeNull();
        }
    }
}
