using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Sale.Ambev.Domain.Entities;

namespace Teste.Sale.Ambev.Unit.Domain.Entities
{
    public class SaleEntityTests
    {
        [Fact]
        public void CancelItem_MarksItemAsCancelled()
        {
            // Arrange
            var sale = new SaleEntity();
            var itemId = Guid.NewGuid();
            sale.Items.Add(new SaleItemEntity
            {
                SaleItemId = itemId,
                ProductId = "1",
                ProductName = "Coca Cola",
                Quantity = 1,
                UnitPrice = 5,
                Discount = 0
            });

            // Act
            sale.CancelItem(itemId);

            // Assert
            sale.Items.First().Cancelled.Should().BeTrue();
        }
    }
}
