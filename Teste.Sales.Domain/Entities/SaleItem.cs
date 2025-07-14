using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste.Sales.Domain.Entities
{
    public class SaleItem
    {
        public SaleItem(string productId, string productName, int quantity, decimal unitPrice)
        {
            if (quantity > 20)
                throw new ArgumentException("Max 20 units per product");

            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Cancelled = false;

            ApplyDiscount();
        }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Total => (UnitPrice * Quantity) - DiscountAmount;
        public bool Cancelled { get; private set; }

        public decimal DiscountAmount => UnitPrice * Quantity * Discount;

        public void ApplyDiscount()
        {
            if (Quantity < 4) Discount = 0;
            else if (Quantity < 10) Discount = 0.10m;
            else if (Quantity <= 20) Discount = 0.20m;
            else throw new Exception("Invalid quantity");
        }

        public void Cancel() => Cancelled = true;
    }

}
