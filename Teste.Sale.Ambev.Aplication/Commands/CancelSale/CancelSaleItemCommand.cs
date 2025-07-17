namespace Teste.Sale.Ambev.Aplication.Commands.CancelSale
{
    public class CancelSaleItemCommand
    {
        public Guid SaleItemId { get; set; }
        public Guid SaleId { get; set; }

        public CancelSaleItemCommand(Guid saleItemId, Guid saleId)
        {
            SaleItemId = saleItemId;
            SaleId = saleId;
        }
    }
}
