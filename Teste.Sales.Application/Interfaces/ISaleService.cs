using Teste.Sales.Application.DTOs;

namespace Teste.Sales.Application.Interfaces
{
    public interface ISaleService
    {
        Guid CreateSale(CreateSaleDto dto);
        SaleDto GetSaleById(Guid id);
        IEnumerable<SaleDto> GetAllSales();
        void UpdateSale(Guid id, UpdateSaleDto dto);
        void CancelSale(Guid id);
        void CancelSaleItem(Guid saleId, string productId);
    }

}