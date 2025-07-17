using Teste.Sale.Ambev.Domain.Repositories;
using Teste.Sale.Ambev.Domain.Entities;

namespace Teste.Sale.Ambev.Aplication.Commands.CancelSale
{
    public class CancelSaleItemCommandHandler
    {
        private readonly ISaleRepository _repository;

        public CancelSaleItemCommandHandler(ISaleRepository repository)
        {
            _repository = repository;
        }

        public void Handle(CancelSaleItemCommand command)
        {
            var saleItem = _repository.GetItemById(command.SaleItemId);
            if (saleItem == null)
                throw new Exception("Item da venda não encontrado");

            var sale = _repository.GetById(saleItem.SaleId);
            if (sale == null)
                throw new Exception("Venda associada não encontrada");

            sale.CancelItem(saleItem.SaleItemId);

            // Persistir
            _repository.Update(sale);
        }
    }
}
