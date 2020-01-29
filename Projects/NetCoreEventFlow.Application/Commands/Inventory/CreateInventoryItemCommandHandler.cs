using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using NetCoreEventFlow.Domain.Inventory;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreEventFlow.Application.Commands.Inventory
{
    public sealed class CreateInventoryItemCommandHandler : CommandHandler<InventoryItemAggregate, InventoryItemId, IExecutionResult, CreateInventoryItemCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(InventoryItemAggregate aggregate, CreateInventoryItemCommand command, CancellationToken cancellationToken)
        {
            var executionResult = aggregate.Init(command.Name);
            return Task.FromResult(executionResult);
        }
    }
}
