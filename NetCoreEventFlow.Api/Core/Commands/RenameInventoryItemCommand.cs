using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using NetCoreEventFlow.Api.Core.Domain;

namespace NetCoreEventFlow.Api.Core.Commands
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
