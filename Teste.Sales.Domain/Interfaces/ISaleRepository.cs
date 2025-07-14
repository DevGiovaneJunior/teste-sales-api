using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Sales.Domain.Entities;

namespace Teste.Sales.Domain.Interfaces
{
    public interface ISaleRepository
    {
        void Add(Sale sale);
        Sale? GetById(Guid id);
        IEnumerable<Sale> GetAll();
        void Update(Sale sale);
        void SaveChanges();
    }
}
