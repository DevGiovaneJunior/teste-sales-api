using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste.Sales.Application.DTOs
{
    public class CreateSaleDto
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string BranchId { get; set; }
        public string BranchName { get; set; }
        public List<CreateSaleItemDto> Items { get; set; }
    }
}
