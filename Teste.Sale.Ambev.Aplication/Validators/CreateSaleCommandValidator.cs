using FluentValidation;
using Teste.Sale.Ambev.Aplication.Commands.CreateSale;

namespace Teste.Sale.Ambev.Aplication.Validators
{
    public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleCommandValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.CustomerName).NotEmpty();
            RuleFor(x => x.BranchId).NotEmpty();
            RuleFor(x => x.BranchName).NotEmpty();
            RuleFor(x => x.Items).NotEmpty().WithMessage("A venda deve conter ao menos um item.");

            RuleForEach(x => x.Items).ChildRules(item =>
            {
                item.RuleFor(i => i.ProductId).NotEmpty();
                item.RuleFor(i => i.ProductName).NotEmpty();
                item.RuleFor(i => i.Quantity)
                    .GreaterThan(0).WithMessage("Quantidade deve ser maior que 0.")
                    .LessThanOrEqualTo(20).WithMessage("Não é permitido vender mais de 20 unidades de um mesmo produto.");
                item.RuleFor(i => i.UnitPrice)
                    .GreaterThan(0).WithMessage("Preço unitário deve ser maior que zero.");
            });
        }
    }
}
