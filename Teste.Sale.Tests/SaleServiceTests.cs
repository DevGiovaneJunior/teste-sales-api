using System;
using System.Collections.Generic;
using Teste.Sales.Application.DTOs;
using Teste.Sales.Application.Services;
using Teste.Sales.Domain.Entities;
using Teste.Sales.Infrastructure.Repositories;
using Xunit;
using Microsoft.Extensions.Logging.Abstractions;

namespace Teste.Sales.Tests
{
    public class SaleServiceTests
    {
        private readonly SaleService _service;

        public SaleServiceTests()
        {
            var repo = new InMemorySaleRepository();
            var logger = new NullLogger<SaleService>();
            _service = new SaleService(repo, logger);
        }

        [Fact]
        public void CreateSale_ShouldCreateSaleAndReturnId()
        {
            // Arrange
            var dto = new CreateSaleDto
            {
                CustomerId = "cust-001",
                CustomerName = "João Silva",
                BranchId = "branch-01",
                BranchName = "Loja Centro",
                Items = new List<CreateSaleItemDto>
                {
                    new CreateSaleItemDto
                    {
                        ProductId = "prod-001",
                        ProductName = "Produto A",
                        Quantity = 5,
                        UnitPrice = 10m
                    },
                    new CreateSaleItemDto
                    {
                        ProductId = "prod-002",
                        ProductName = "Produto B",
                        Quantity = 2,
                        UnitPrice = 20m
                    }
                }
            };

            // Act
            var saleId = _service.CreateSale(dto);

            // Assert
            Assert.NotEqual(Guid.Empty, saleId);
        }
    }
}
