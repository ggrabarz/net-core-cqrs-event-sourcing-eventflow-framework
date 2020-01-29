using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using NetCoreEventFlow.Domain.Inventory;

namespace NetCoreEventFlow.Application.Commands.Inventory
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
