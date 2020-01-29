using EventFlow;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Queries;
using Microsoft.AspNetCore.Mvc;
using NetCoreEventFlow.Application.Commands.Inventory;
using NetCoreEventFlow.Domain.Inventory;
using NetCoreEventFlow.ReadModel.DomainEventHandlers.Inventory;
using NetCoreEventFlow.ReadModel.Queries.Inventory;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreEventFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class ValuesController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;


        public ValuesController(ICommandBus commandBus,
            IQueryProcessor queryProcessor)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }

        [HttpGet]
        public async Task<IActionResult> GetInventoryItems()
        {
            return Ok(await _queryProcessor.ProcessAsync(new GetInventoryItemsListQuery(), CancellationToken.None));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemDetails([FromRoute] string id)
        {
            var result = await _queryProcessor.ProcessAsync(new ReadModelByIdQuery<InventoryItemDetailsReadModel>(new InventoryItemId(id)), CancellationToken.None);
            if (result == null || result.Activated == false)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem([FromQuery] string name)
        {
            var newItemId = InventoryItemId.New;
            var result = await _commandBus.PublishAsync(new CreateInventoryItemCommand(newItemId, name), CancellationToken.None);
            return result.IsSuccess ? (IActionResult)StatusCode(201, newItemId.ToString()) : BadRequest();
        }

        [HttpPost("{id}/ChangeName")]
        public async Task<IActionResult> ChangeName([FromRoute] string id, [FromQuery] string name)
        {
            var result = await _commandBus.PublishAsync(new RenameInventoryItemCommand(new InventoryItemId(id), name), CancellationToken.None);
            return result.IsSuccess ? (IActionResult)NoContent() : BadRequest();
        }

        [HttpPost("{id}/IncreaseAmmount")]
        public async Task<IActionResult> CheckIn([FromRoute] string id, [FromQuery] int number)
        {
            var result = await _commandBus.PublishAsync(new CheckInItemsToInventoryCommand(new InventoryItemId(id), number), CancellationToken.None);
            if (!result.IsSuccess)
            {
                foreach(var error in (result as FailedExecutionResult).Errors)
                {
                    ModelState.AddModelError("number", error);
                }
                return BadRequest(ModelState);
            }
            return NoContent();
        }

        [HttpPost("{id}/DecreaseAmmount")]
        public async Task<IActionResult> Remove([FromRoute] string id, [FromQuery] int number)
        {
            var result = await _commandBus.PublishAsync(new RemoveItemsFromInventoryCommand(new InventoryItemId(id), number), CancellationToken.None);
            return result.IsSuccess ? (IActionResult)NoContent() : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deactivate([FromRoute] string id)
        {
            var result = await _commandBus.PublishAsync(new DeactivateInventoryItemCommand(new InventoryItemId(id)), CancellationToken.None);
            return result.IsSuccess ? (IActionResult)NoContent() : BadRequest();
        }
    }
}
