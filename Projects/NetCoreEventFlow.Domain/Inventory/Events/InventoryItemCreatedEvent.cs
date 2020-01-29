using EventFlow.Aggregates;
using EventFlow.EventStores;

namespace NetCoreEventFlow.Domain.Inventory.Events
{
    [EventVersion("InventoryItemCreated", 1)]
    public sealed class InventoryItemCreatedEvent : AggregateEvent<InventoryItemAggregate, InventoryItemId>
    {
        public string Name { get; }

        public InventoryItemCreatedEvent(string name)
        {
            Name = name;
        }
    }
}

