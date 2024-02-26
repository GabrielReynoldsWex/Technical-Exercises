using MediatR;
using MediatrExercisev2.Abstraction.Requests.Item;
using MediatrExercisev2.Application.Items.Commands;
using MediatrExercisev2.Application.Items.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MediatrExercisev2.Controllers
{
    [Route("api/item")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ItemController(IMediator mediator) 
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] CreateItemRequest request)
        {
            float price = float.Parse(request.Price);
            var item = await _mediator.Send(new CreateItemCommand(request.Name, request.Category, price));
            return Ok(item);
        }

        [HttpGet]
        public async Task<ActionResult> GetItems() 
        {
            var items = await _mediator.Send(new GetItemsQuery());
            return Ok(items);   
        }
    }
}
