using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using NetCoreEventFlow.Api.Core.Domain;
using NetCoreEventFlow.Api.Core.Commands;

namespace NetCoreEventFlow.Api.Core.CommandHandlers
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
