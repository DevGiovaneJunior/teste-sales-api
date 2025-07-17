using System;
using System.Linq;
using Moq;
using Xunit;  // Importar xUnit para [Fact]
using Teste.Sale.Ambev.Aplication.Commands.CancelSale;
using Teste.Sale.Ambev.Domain.Entities;
using Teste.Sale.Ambev.Domain.Repositories;

namespace Teste.Sale.Ambev.Unit.Application.Commands
{
    public class CancelSaleItemCommandHandlerTests
    {

        [Fact]
        public void Handle_Should_Cancel_Item_Successfully()
        {
            // Arrange
            var sale = new SaleEntity(); 
            var saleId = sale.SaleId; 
            var saleItemId = Guid.NewGuid();

            sale.Items.Add(new SaleItemEntity
            {
                SaleItemId = saleItemId,
                SaleId = saleId,
                Cancelled = false,
            });

            var mockRepo = new Mock<ISaleRepository>();

            mockRepo.Setup(r => r.GetItemById(It.IsAny<Guid>()))
                .Returns((Guid id) => sale.Items.FirstOrDefault(i => i.SaleItemId == id));

            mockRepo.Setup(r => r.GetById(It.IsAny<Guid>()))
                .Returns((Guid id) => id == saleId ? sale : null);

            var handler = new CancelSaleItemCommandHandler(mockRepo.Object);

            // Act
            handler.Handle(new CancelSaleItemCommand(saleItemId, saleId));

            // Assert
            Assert.True(sale.Items[0].Cancelled);
            mockRepo.Verify(r => r.Update(sale), Times.Once);
        }


        [Fact]
        public void Handle_Should_Throw_When_Item_Not_Found()
        {
            var mockRepo = new Mock<ISaleRepository>();
            mockRepo.Setup(r => r.GetItemById(It.IsAny<Guid>())).Returns<SaleItemEntity?>(null);

            var handler = new CancelSaleItemCommandHandler(mockRepo.Object);

            Assert.Throws<Exception>(() =>
                handler.Handle(new CancelSaleItemCommand(Guid.NewGuid(), Guid.NewGuid())));
        }

        [Fact]
        public void Handle_Should_Throw_When_Sale_Not_Found()
        {
            var saleItemId = Guid.NewGuid();

            var item = new SaleItemEntity
            {
                SaleItemId = saleItemId,
                SaleId = Guid.NewGuid()
            };

            var mockRepo = new Mock<ISaleRepository>();
            mockRepo.Setup(r => r.GetItemById(saleItemId)).Returns(item);
            mockRepo.Setup(r => r.GetById(It.IsAny<Guid>())).Returns<SaleEntity?>(null);

            var handler = new CancelSaleItemCommandHandler(mockRepo.Object);

            Assert.Throws<Exception>(() =>
                handler.Handle(new CancelSaleItemCommand(saleItemId, Guid.NewGuid())));
        }
    }
}
