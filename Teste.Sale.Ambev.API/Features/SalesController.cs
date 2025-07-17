using Microsoft.AspNetCore.Mvc;
using Teste.Sale.Ambev.Aplication.Commands.CancelSale;
using Teste.Sale.Ambev.Aplication.Commands.CreateSale;
using Teste.Sale.Ambev.Aplication.Queries;

namespace Teste.Sale.Ambev.API.Features
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly CreateSaleCommandHandler _createSaleCommandHandler;
        private readonly GetSaleByIdHandler _getSaleByIdHandler;
        private readonly GetAllSalesHandler _getAllSalesHandler;
        private readonly CancelSaleHandler _cancelSaleHandler;
        private readonly CancelSaleItemCommandHandler _cancelSaleItemHandler;

        public SalesController(
            CreateSaleCommandHandler createSaleCommandHandler,
            GetSaleByIdHandler getSaleByIdHandler,
            GetAllSalesHandler getAllSalesHandler,
            CancelSaleHandler cancelSaleHandler,
            CancelSaleItemCommandHandler cancelSaleItemHandler)
        {
            _createSaleCommandHandler = createSaleCommandHandler;
            _getSaleByIdHandler = getSaleByIdHandler;
            _getAllSalesHandler = getAllSalesHandler;
            _cancelSaleHandler = cancelSaleHandler;
            _cancelSaleItemHandler = cancelSaleItemHandler;
        }

        [HttpPost]
        public IActionResult CreateSale([FromBody] CreateSaleCommand command)
        {
            var result = _createSaleCommandHandler.Handle(command);
            return CreatedAtAction(nameof(GetById), new { id = result }, null);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var sale = _getSaleByIdHandler.Handle(new GetSaleByIdQuery(id));
            if (sale == null) return NotFound();
            return Ok(sale);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var sales = _getAllSalesHandler.Handle(new GetAllSalesQuery());
            return Ok(sales);
        }

        [HttpPut("{id}/cancel")]
        public IActionResult Cancel(Guid id)
        {
            _cancelSaleHandler.Handle(new CancelSaleCommand(id));
            return NoContent();
        }

        [HttpPut("{saleId}/items/{saleItemId}/cancel")]
        public IActionResult CancelItem(Guid saleId, Guid saleItemId)
        {
            _cancelSaleItemHandler.Handle(new CancelSaleItemCommand(saleItemId, saleId));
            return NoContent();
        }
    }
}
