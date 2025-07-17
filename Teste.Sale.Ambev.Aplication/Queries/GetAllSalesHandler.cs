using Teste.Sale.Ambev.Domain.Entities;
using Teste.Sale.Ambev.Domain.Repositories;

namespace Teste.Sale.Ambev.Aplication.Queries
{
    public class GetAllSalesHandler
    {
        private readonly ISaleRepository _repository;

        public GetAllSalesHandler(ISaleRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<SaleEntity> Handle(GetAllSalesQuery query)
        {
            return _repository.GetAll();
        }
    }
}
