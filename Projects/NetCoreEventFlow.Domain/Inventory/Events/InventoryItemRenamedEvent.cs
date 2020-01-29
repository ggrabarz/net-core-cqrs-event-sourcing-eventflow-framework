using EventFlow.Aggregates;
using EventFlow.EventStores;

namespace NetCoreEventFlow.Domain.Inventory.Events
{
    [EventVersion("InventoryItemRenamed", 1)]
    public sealed class InventoryItemRenamedEvent : AggregateEvent<InventoryItemAggregate, InventoryItemId>
    {
        public string NewName { get; }

        public InventoryItemRenamedEvent(string newName)
        {
            NewName = newName;
        }
    }
}

