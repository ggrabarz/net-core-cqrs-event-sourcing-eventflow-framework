using EventFlow.Aggregates;
using EventFlow.EventStores;

namespace NetCoreEventFlow.Domain.Inventory.Events
{
    [EventVersion("InventoryItemDeactivated", 1)]
    public sealed class InventoryItemDeactivatedEvent : AggregateEvent<InventoryItemAggregate, InventoryItemId>
    {
    }
}

