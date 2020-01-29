using EventFlow.Aggregates;
using EventFlow.EventStores;

namespace NetCoreEventFlow.Domain.Inventory.Events
{
    [EventVersion("ItemsCheckedInToInventory", 1)]
    public sealed class ItemsCheckedInToInventoryEvent : AggregateEvent<InventoryItemAggregate, InventoryItemId>
    {
        public int Count { get; }

        public ItemsCheckedInToInventoryEvent(int count)
        {
            Count = count;
        }
    }
}

