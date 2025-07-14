using Microsoft.Extensions.Logging;
using Teste.Sales.Application.DTOs;
using Teste.Sales.Application.Interfaces;
using Teste.Sales.Domain.Entities;
using Teste.Sales.Domain.Interfaces;

namespace Teste.Sales.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _repository;
        private readonly ILogger<SaleService> _logger;

        public SaleService(ISaleRepository repository, ILogger<SaleService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public Guid CreateSale(CreateSaleDto dto)
        {
            var sale = new Sale(
                dto.CustomerId,
                dto.CustomerName,
                dto.BranchId,
                dto.BranchName
            );

            foreach (var itemDto in dto.Items)
            {
                var item = new SaleItem(
                    productId: itemDto.ProductId,
                    productName: itemDto.ProductName,
                    quantity: itemDto.Quantity,
                    unitPrice: itemDto.UnitPrice
                );

                sale.AddItem(item);
            }

            _repository.Add(sale);
            _repository.SaveChanges();

            _logger.LogInformation("[EVENT] SaleCreated: {SaleId}", sale.Id);

            return sale.Id;
        }

        public SaleDto GetSaleById(Guid id)
        {
            var sale = _repository.GetById(id);
            if (sale == null) return null;

            return MapToDto(sale);
        }

        public IEnumerable<SaleDto> GetAllSales()
        {
            var sales = _repository.GetAll();
            return sales.Select(s => MapToDto(s));
        }

        public void UpdateSale(Guid id, UpdateSaleDto dto)
        {
            var sale = _repository.GetById(id);
            if (sale == null) throw new Exception("Sale not found");

            sale.ClearItems();

            foreach (var itemDto in dto.Items)
            {
                var item = new SaleItem(
                    productId: itemDto.ProductId,
                    productName: itemDto.ProductName,
                    quantity: itemDto.Quantity,
                    unitPrice: itemDto.UnitPrice
                );

                sale.AddItem(item);
            }

            _repository.Update(sale);
            _repository.SaveChanges();

            _logger.LogInformation("[EVENT] SaleModified: {SaleId}", sale.Id);
        }

        public void CancelSale(Guid id)
        {
            var sale = _repository.GetById(id);
            if (sale == null) throw new Exception("Sale not found");

            sale.Cancel();
            _repository.Update(sale);
            _repository.SaveChanges();

            _logger.LogInformation("[EVENT] SaleCancelled: {SaleId}", sale.Id);
        }

        public void CancelSaleItem(Guid saleId, string productId)
        {
            var sale = _repository.GetById(saleId);
            if (sale == null) throw new Exception("Sale not found");

            var item = sale.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item == null) throw new Exception("Item not found");

            item.Cancel();

            _repository.Update(sale);
            _repository.SaveChanges();

            _logger.LogInformation("[EVENT] ItemCancelled: {ProductId} on Sale {SaleId}", productId, saleId);
        }

        // 🔁 DTO Mapping helper
        private static SaleDto MapToDto(Sale sale)
        {
            return new SaleDto
            {
                Id = sale.Id,
                SaleNumber = sale.SaleNumber,
                Date = sale.Date,
                CustomerName = sale.CustomerName,
                TotalAmount = sale.TotalAmount,
                Cancelled = sale.Cancelled,
                Items = sale.Items.Select(i => new SaleItemDto
                {
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Discount = i.Discount,
                    Total = i.Total,
                    Cancelled = i.Cancelled
                }).ToList()
            };
        }
    }
}
