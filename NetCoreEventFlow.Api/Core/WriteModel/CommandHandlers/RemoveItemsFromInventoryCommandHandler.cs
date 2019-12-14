using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using NetCoreEventFlow.Api.Core.Domain;
using NetCoreEventFlow.Api.Core.Commands;

namespace NetCoreEventFlow.Api.Core.CommandHandlers
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
