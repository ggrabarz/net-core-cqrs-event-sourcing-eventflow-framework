using EventFlow;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Queries;
using Microsoft.AspNetCore.Mvc;
using NetCoreEventFlow.Api.Core.Commands;
using NetCoreEventFlow.Api.Core.Domain;
using NetCoreEventFlow.Api.Core.ReadModel;
using NetCoreEventFlow.Api.Extras.Queries;
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
        public async Task<ActionResult> GetInventoryItems()
        {
            return Ok(await _queryProcessor.ProcessAsync(new GetInventoryItemsListQuery(), CancellationToken.None));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetItemDetails([FromRoute] string id)
        {
            var result = await _queryProcessor.ProcessAsync(new ReadModelByIdQuery<InventoryItemDetailsReadModel>(new InventoryItemId(id)), CancellationToken.None);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddItem([FromQuery] string name)
        {
            var newItemId = InventoryItemId.New;
            await _commandBus.PublishAsync(new CreateInventoryItemCommand(newItemId, name), CancellationToken.None);
            return StatusCode(201, newItemId.ToString());
        }

        [HttpPost("{id}/ChangeName")]
        public async Task<ActionResult> ChangeName([FromRoute] string id, [FromQuery] string name)
        {
            await _commandBus.PublishAsync(new RenameInventoryItemCommand(new InventoryItemId(id), name), CancellationToken.None);
            return NoContent();
        }

        [HttpPost("{id}/IncreaseAmmount")]
        public async Task<ActionResult> CheckIn([FromRoute] string id, [FromQuery] int number)
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
        public async Task<ActionResult> Remove([FromRoute] string id, [FromQuery] int number)
        {
            await _commandBus.PublishAsync(new RemoveItemsFromInventoryCommand(new InventoryItemId(id), number), CancellationToken.None);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Deactivate([FromRoute] string id)
        {
            await _commandBus.PublishAsync(new DeactivateInventoryItemCommand(new InventoryItemId(id)), CancellationToken.None);
            return NoContent();
        }
    }
}
