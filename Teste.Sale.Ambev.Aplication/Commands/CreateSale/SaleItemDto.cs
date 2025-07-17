namespace Teste.Sale.Ambev.Aplication.Commands.CreateSale
{
    public class SaleItemDto
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
