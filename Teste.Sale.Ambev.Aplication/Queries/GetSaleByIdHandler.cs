using Teste.Sale.Ambev.Domain.Entities;
using Teste.Sale.Ambev.Domain.Repositories;

namespace Teste.Sale.Ambev.Aplication.Queries
{
    public class GetSaleByIdHandler
    {
        private readonly ISaleRepository _repository;

        public GetSaleByIdHandler(ISaleRepository repository)
        {
            _repository = repository;
        }

        public SaleEntity? Handle(GetSaleByIdQuery query)
        {
            return _repository.GetById(query.Id);
        }
    }
}
