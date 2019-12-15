using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using NetCoreEventFlow.Api.Core.Domain;

namespace NetCoreEventFlow.Api.Core.Commands
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
