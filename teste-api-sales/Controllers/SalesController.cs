using Microsoft.AspNetCore.Mvc;
using Teste.Sales.Application.Interfaces;
using Teste.Sales.Application.DTOs;

namespace teste_api_sales.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _service;

        public SalesController(ISaleService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult CreateSale([FromBody] CreateSaleDto dto)
        {
            var id = _service.CreateSale(dto);
            return CreatedAtAction(nameof(GetSaleById), new { id }, null);
        }

        [HttpGet("{id}")]
        public IActionResult GetSaleById(Guid id)
        {
            var sale = _service.GetSaleById(id);
            return sale is null ? NotFound() : Ok(sale);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var sales = _service.GetAllSales();
            return Ok(sales);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSale(Guid id, [FromBody] UpdateSaleDto dto)
        {
            _service.UpdateSale(id, dto);
            return NoContent();
        }

        [HttpPost("{id}/cancel")]
        public IActionResult CancelSale(Guid id)
        {
            _service.CancelSale(id);
            return NoContent();
        }

        [HttpPost("{saleId}/items/{productId}/cancel")]
        public IActionResult CancelItem(Guid saleId, string productId)
        {
            _service.CancelSaleItem(saleId, productId);
            return NoContent();
        }
    }
}
