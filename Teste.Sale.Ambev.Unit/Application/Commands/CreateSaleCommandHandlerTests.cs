using FluentAssertions;
using FluentValidation;
using Moq;
using System;
using System.Collections.Generic;
using Teste.Sale.Ambev.Aplication.Commands.CreateSale;
using Teste.Sale.Ambev.Aplication.Validators;
using Teste.Sale.Ambev.Domain.Entities;
using Teste.Sale.Ambev.Domain.Repositories;
using Xunit;

namespace Teste.Sale.Ambev.Unit.Application.Commands
{
    public class CreateSaleCommandHandlerTests
    {
        private readonly Mock<ISaleRepository> _repositoryMock;
        private readonly CreateSaleCommandHandler _handler;
        private readonly IValidator<CreateSaleCommand> _validator;

        public CreateSaleCommandHandlerTests()
        {
            _repositoryMock = new Mock<ISaleRepository>();
            _validator = new CreateSaleCommandValidator();
            _handler = new CreateSaleCommandHandler(_repositoryMock.Object, _validator);
        }

        [Fact]
        public void Handle_Should_Create_Sale_Successfully()
        {
            // Arrange
            var command = new CreateSaleCommand
            {
                CustomerId = "1",
                CustomerName = "Giovane Teste",
                BranchId = "1",
                BranchName = "Empresa Teste",
                Items = new List<SaleItemDto>
                {
                    new SaleItemDto
                    {
                        ProductId = "1",
                        ProductName = "Produto Teste",
                        Quantity = 2,
                        UnitPrice = 10,
                    }
                }
            };

            // Act
            var result = _handler.Handle(command);

            // Assert
            result.Should().NotBeEmpty();
            _repositoryMock.Verify(r => r.Save(It.Is<SaleEntity>(sale =>
                sale.CustomerId == "1" &&
                sale.CustomerName == "Giovane Teste" &&
                sale.BranchId == "1" &&
                sale.BranchName == "Empresa Teste" &&
                sale.Items.Count == 1 &&
                sale.Items[0].ProductId == "1"
            )), Times.Once);
        }

        [Fact]
        public void Handle_Should_Call_Repository_Save_Once()
        {
            var command = new CreateSaleCommand
            {
                CustomerId = "1",
                CustomerName = "Giovane Teste",
                BranchId = "1",
                BranchName = "Empresa Teste",
                Items = new List<SaleItemDto>
                {
                    new SaleItemDto
                    {
                        ProductId = "1",
                        ProductName = "Produto Teste",
                        Quantity = 1,
                        UnitPrice = 20,
                    }
                }
            };

            _handler.Handle(command);

            _repositoryMock.Verify(r => r.Save(It.IsAny<SaleEntity>()), Times.Once);
        }
    }
}