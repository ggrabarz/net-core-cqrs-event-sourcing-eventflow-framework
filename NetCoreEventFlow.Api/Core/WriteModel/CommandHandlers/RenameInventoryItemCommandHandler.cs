using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using NetCoreEventFlow.Api.Core.Domain;
using NetCoreEventFlow.Api.Core.Commands;

namespace NetCoreEventFlow.Api.Core.CommandHandlers
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
