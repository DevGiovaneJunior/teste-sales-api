namespace Teste.Sale.Ambev.Aplication.Commands.CancelSale;
public class CancelSaleCommand
{
    public Guid SaleId { get; set; }

    public CancelSaleCommand(Guid saleId)
    {
        SaleId = saleId;
    }
}