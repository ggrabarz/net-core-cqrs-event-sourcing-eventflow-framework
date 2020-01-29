using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using NetCoreEventFlow.Domain.Inventory;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreEventFlow.Application.Commands.Inventory
{
    public sealed class CheckInItemsToInventoryCommandHandler : CommandHandler<InventoryItemAggregate, InventoryItemId, IExecutionResult, CheckInItemsToInventoryCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(InventoryItemAggregate aggregate, CheckInItemsToInventoryCommand command, CancellationToken cancellationToken)
        {
            var executionResult = aggregate.CheckIn(command.Count);
            return Task.FromResult(executionResult);
        }
    }
}
