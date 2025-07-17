namespace Teste.Sale.Ambev.Domain.Entities
{
    public class SaleItemEntity
    {
        public Guid SaleItemId { get; set; }
        public Guid SaleId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public bool Cancelled { get; set; } = false;

        public decimal Total => Quantity * UnitPrice * (1 - Discount);
    }
}
