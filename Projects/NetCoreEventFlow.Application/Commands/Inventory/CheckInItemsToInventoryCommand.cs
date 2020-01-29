using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using NetCoreEventFlow.Domain.Inventory;

namespace NetCoreEventFlow.Application.Commands.Inventory
{
    [CommandVersion("CheckInItemsToInventory", 1)]
    public sealed class CheckInItemsToInventoryCommand : Command<InventoryItemAggregate, InventoryItemId, IExecutionResult>
    {
        public InventoryItemId InventoryItemId { get; }
        public int Count { get; }

        public CheckInItemsToInventoryCommand(InventoryItemId inventoryItemId, int count) : base(inventoryItemId)
        {
            InventoryItemId = inventoryItemId;
            Count = count;
        }
    }
}
