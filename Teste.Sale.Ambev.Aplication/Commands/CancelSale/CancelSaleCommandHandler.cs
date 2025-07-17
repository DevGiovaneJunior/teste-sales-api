using Teste.Sale.Ambev.Domain.Repositories;

namespace Teste.Sale.Ambev.Aplication.Commands.CancelSale
{
    public class CancelSaleHandler
    {
        private readonly ISaleRepository _repository;

        public CancelSaleHandler(ISaleRepository repository)
        {
            _repository = repository;
        }

        public void Handle(CancelSaleCommand command)
        {
            var sale = _repository.GetById(command.SaleId);
            if (sale == null) return;

            sale.Cancel();
            _repository.Update(sale);
        }
    }
}
