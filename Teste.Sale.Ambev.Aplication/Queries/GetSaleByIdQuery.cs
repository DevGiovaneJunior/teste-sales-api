using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste.Sale.Ambev.Aplication.Queries
{
    public class GetSaleByIdQuery
    {
        public Guid Id { get; set; }

        public GetSaleByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
