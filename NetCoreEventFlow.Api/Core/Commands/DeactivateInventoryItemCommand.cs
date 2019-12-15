using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using NetCoreEventFlow.Api.Core.Domain;

namespace NetCoreEventFlow.Api.Core.Commands
{
    [CommandVersion("DeactivateInventoryItem", 1)]
    public sealed class DeactivateInventoryItemCommand : Command<InventoryItemAggregate, InventoryItemId, IExecutionResult>
    {
        public InventoryItemId InventoryItemId { get; }

        public DeactivateInventoryItemCommand(InventoryItemId inventoryItemId) : base(inventoryItemId)
        {
            InventoryItemId = inventoryItemId;
        }
    }
}
