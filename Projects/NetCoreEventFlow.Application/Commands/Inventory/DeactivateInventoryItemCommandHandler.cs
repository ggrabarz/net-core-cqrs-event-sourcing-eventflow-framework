using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using NetCoreEventFlow.Domain.Inventory;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreEventFlow.Application.Commands.Inventory
{
    public sealed class DeactivateInventoryItemCommandHandler : CommandHandler<InventoryItemAggregate, InventoryItemId, IExecutionResult, DeactivateInventoryItemCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(InventoryItemAggregate aggregate, DeactivateInventoryItemCommand command, CancellationToken cancellationToken)
        {
            var executionResult = aggregate.Deactivate();
            return Task.FromResult(executionResult);
        }
    }
}
