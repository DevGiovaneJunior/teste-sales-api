using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste.Sale.Ambev.Domain.Entities
{
    public class SaleEntity
    {
        public Guid SaleId { get; private set; } = Guid.NewGuid();
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string BranchId { get; set; }
        public string BranchName { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public List<SaleItemEntity> Items { get; set; } = new();
        public bool Cancelled { get; private set; } = false;

        public decimal Total => Items.Where(i => !i.Cancelled).Sum(i => i.Total);

        public void Cancel() => Cancelled = true;

        public void CancelItem(Guid saleIdItem)
        {
            var item = Items.FirstOrDefault(i => i.SaleItemId == saleIdItem);
            if (item != null)
            {
                item.Cancelled = true;
            }
        }
    }
}
