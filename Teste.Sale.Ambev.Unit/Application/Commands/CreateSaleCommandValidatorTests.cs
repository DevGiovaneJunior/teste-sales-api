using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Sale.Ambev.Aplication.Commands.CreateSale;
using Teste.Sale.Ambev.Aplication.Validators;

namespace Teste.Sale.Ambev.Unit.Application.Commands
{
    public class CreateSaleCommandValidatorTests
    {
        private readonly CreateSaleCommandValidator _validator = new();

        [Fact]
        public void Should_Fail_When_Items_Is_Null()
        {
            var command = new CreateSaleCommand
            {
                CustomerId = "1",
                CustomerName = "Giovane Teste",
                BranchId = "1",
                BranchName = "Empresa Teste",
                Items = null
            };

            var result = _validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.PropertyName == "Items" && e.ErrorMessage == "A venda deve conter ao menos um item.");
        }

        [Fact]
        public void Should_Fail_When_Items_Is_Empty()
        {
            var command = new CreateSaleCommand
            {
                CustomerId = "1",
                CustomerName = "Giovane Teste",
                BranchId = "1",
                BranchName = "Empresa Teste",
                Items = new List<SaleItemDto>()
            };

            var result = _validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.PropertyName == "Items" && e.ErrorMessage == "A venda deve conter ao menos um item.");
        }
    }
}
