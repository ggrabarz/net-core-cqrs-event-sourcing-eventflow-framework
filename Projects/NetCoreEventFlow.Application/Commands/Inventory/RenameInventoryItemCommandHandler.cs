using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using NetCoreEventFlow.Domain.Inventory;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreEventFlow.Application.Commands.Inventory
{
    public sealed class RenameInventoryItemCommandHandler : CommandHandler<InventoryItemAggregate, InventoryItemId, IExecutionResult, RenameInventoryItemCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(InventoryItemAggregate aggregate, RenameInventoryItemCommand command, CancellationToken cancellationToken)
        {
            var executionResult = aggregate.ChangeName(command.NewName);
            return Task.FromResult(executionResult);
        }
    }
}
