using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;
using NetCoreEventFlow.Api.Core.Domain;
using NetCoreEventFlow.Api.Core.Commands;

namespace NetCoreEventFlow.Api.Core.CommandHandlers
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
