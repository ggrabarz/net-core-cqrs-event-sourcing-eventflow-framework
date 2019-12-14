using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using NetCoreEventFlow.Api.Core.Domain;

namespace NetCoreEventFlow.Api.Core.Commands
{
    [CommandVersion("CreateInventoryItem", 1)]
    public sealed class CreateInventoryItemCommand : Command<InventoryItemAggregate, InventoryItemId, IExecutionResult>
    {
        public InventoryItemId InventoryItemId { get; }
        public string Name { get; }

        public CreateInventoryItemCommand(InventoryItemId inventoryItemId, string name) : base(inventoryItemId)
        {
            InventoryItemId = inventoryItemId;
            Name = name;
        }
    }
}
