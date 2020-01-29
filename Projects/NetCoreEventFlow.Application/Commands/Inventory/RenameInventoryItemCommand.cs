using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using NetCoreEventFlow.Domain.Inventory;

namespace NetCoreEventFlow.Application.Commands.Inventory
{
    [CommandVersion("RenameInventoryItem", 1)]
    public sealed class RenameInventoryItemCommand : Command<InventoryItemAggregate, InventoryItemId, IExecutionResult>
    {
        public InventoryItemId InventoryItemId { get; }
        public string NewName { get; }

        public RenameInventoryItemCommand(InventoryItemId inventoryItemId, string newName) : base(inventoryItemId)
        {
            InventoryItemId = inventoryItemId;
            NewName = newName;
        }
    }
}
