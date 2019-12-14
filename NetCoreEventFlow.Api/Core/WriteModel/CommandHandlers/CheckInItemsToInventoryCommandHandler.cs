using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using NetCoreEventFlow.Api.Core.Domain;
using NetCoreEventFlow.Api.Core.Commands;

namespace NetCoreEventFlow.Api.Core.CommandHandlers
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
