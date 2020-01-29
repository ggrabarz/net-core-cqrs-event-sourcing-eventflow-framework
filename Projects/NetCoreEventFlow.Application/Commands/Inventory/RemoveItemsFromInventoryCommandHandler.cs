using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using NetCoreEventFlow.Domain.Inventory;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreEventFlow.Application.Commands.Inventory
{
    public sealed class RemoveItemsFromInventoryCommandHandler : CommandHandler<InventoryItemAggregate, InventoryItemId, IExecutionResult, RemoveItemsFromInventoryCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(InventoryItemAggregate aggregate, RemoveItemsFromInventoryCommand command, CancellationToken cancellationToken)
        {
            var executionResult = aggregate.Remove(command.Count);
            return Task.FromResult(executionResult);
        }
    }
}
