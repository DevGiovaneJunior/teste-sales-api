using FluentValidation;
using Teste.Sale.Ambev.Domain;
using Teste.Sale.Ambev.Domain.Entities;
using Teste.Sale.Ambev.Domain.Exceptions;
using Teste.Sale.Ambev.Domain.Repositories;

namespace Teste.Sale.Ambev.Aplication.Commands.CreateSale;

public class CreateSaleCommandHandler
{
    private readonly ISaleRepository _repository;
    private readonly IValidator<CreateSaleCommand> _validator;

    public CreateSaleCommandHandler(ISaleRepository repository,  IValidator<CreateSaleCommand> validator)
    {
        _repository = repository; _validator = validator;
    }

    public Guid Handle(CreateSaleCommand command)
    {
        foreach (var item in command.Items)
        {
            if (item.Quantity > 20)
            {
                throw new BusinessRuleException($"Não é permitido vender mais de 20 unidades do produto {item.ProductName}.");
            }
        }

        var sale = new SaleEntity
        {
            CustomerId = command.CustomerId,
            CustomerName = command.CustomerName,
            BranchId = command.BranchId,
            BranchName = command.BranchName
        };

        foreach (var item in command.Items)
        {
            sale.Items.Add(new SaleItemEntity
            {
                SaleItemId = Guid.NewGuid(),
                SaleId = sale.SaleId,
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                Discount = CalculateDiscount(item.Quantity),
                Cancelled = false
            });
        }

        _repository.Save(sale);

        return sale.SaleId;
    }
    private decimal CalculateDiscount(int quantity)
    {
        if (quantity >= 10 && quantity <= 20) return 0.2m;
        if (quantity >= 4) return 0.1m;
        return 0.0m;
    }
}