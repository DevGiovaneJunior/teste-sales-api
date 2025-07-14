using System;
using System.Collections.Generic;
using System.Linq;
using Teste.Sales.Domain.Entities;
using Teste.Sales.Domain.Interfaces;

namespace Teste.Sales.Infrastructure.Repositories
{
    public class InMemorySaleRepository : ISaleRepository
    {
        private readonly List<Sale> _sales = new();

        public void Add(Sale sale)
        {
            _sales.Add(sale);
        }

        public Sale? GetById(Guid id)
        {
            return _sales.FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<Sale> GetAll()
        {
            return _sales;
        }

        public void Update(Sale sale)
        {
            var index = _sales.FindIndex(s => s.Id == sale.Id);
            if (index >= 0)
                _sales[index] = sale;
        }

        public void SaveChanges()
        {
            // Nada a fazer aqui, pois está tudo em memória.
        }
    }
}
