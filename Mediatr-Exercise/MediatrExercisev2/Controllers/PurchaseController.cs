using MediatR;
using MediatrExercisev2.Abstraction.Requests.Purchase;
using MediatrExercisev2.Application.Customers.Queries;
using MediatrExercisev2.Application.Purchases.Commands;
using MediatrExercisev2.Application.Purchases.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MediatrExercisev2.Controllers
{
    [Route("api/purchase")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PurchaseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePurchase([FromBody] CreatePurchaseRequest request) 
        {
            var result = await _mediator.Send(new CreatePurchaseCommand(request.CustomerID, request.ProductID));
            return Ok(result);
        } 

        [HttpGet]
        public async Task<IActionResult> GetPurchasesByCustomerId([FromQuery] string customerId, string categoryFilter, float priceFilter)
        {
            var customer = await _mediator.Send(new GetCustomerDiscountByIdQuery(customerId));

            if (categoryFilter != null)
            {
                var result = await _mediator.Send(new GetPurchasesByCustomerIdQuery(customerId, customer.CustomerDiscount, categoryFilter, priceFilter));
                return Ok(result);
            }
            else 
            {
                var result = await _mediator.Send(new GetPurchasesByCustomerIdQuery(customerId, customer.CustomerDiscount));
                return Ok(result);
            }
        }
    }
}