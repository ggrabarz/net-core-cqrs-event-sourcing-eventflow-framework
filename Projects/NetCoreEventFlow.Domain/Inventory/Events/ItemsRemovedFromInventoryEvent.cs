using EventFlow.Aggregates;
using EventFlow.EventStores;

namespace NetCoreEventFlow.Domain.Inventory.Events
{
    [EventVersion("ItemsRemovedFromInventory", 1)]
    public sealed class ItemsRemovedFromInventoryEvent : AggregateEvent<InventoryItemAggregate, InventoryItemId>
    {
        public int Count { get; }

        public ItemsRemovedFromInventoryEvent(int count)
        {
            Count = count;
        }
    }
}

