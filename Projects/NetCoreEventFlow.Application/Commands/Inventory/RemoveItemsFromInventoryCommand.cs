using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using NetCoreEventFlow.Domain.Inventory;

namespace NetCoreEventFlow.Application.Commands.Inventory
{
    [CommandVersion("RemoveItemsFromInventory", 1)]
    public sealed class RemoveItemsFromInventoryCommand : Command<InventoryItemAggregate, InventoryItemId, IExecutionResult>
    {
        public InventoryItemId InventoryItemId { get; }
        public int Count { get; }

        public RemoveItemsFromInventoryCommand(InventoryItemId inventoryItemId, int count) : base(inventoryItemId)
        {
            InventoryItemId = inventoryItemId;
            Count = count;
        }
    }
}
