namespace Teste.Sale.Ambev.Aplication.Commands.CreateSale;
public class CreateSaleCommand
{
    public string CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string BranchId { get; set; }
    public string BranchName { get; set; }
    public List<SaleItemDto> Items { get; set; }
}

