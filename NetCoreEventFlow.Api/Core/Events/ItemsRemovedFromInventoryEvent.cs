using EventFlow.Aggregates;
using EventFlow.EventStores;
using NetCoreEventFlow.Api.Core.Domain;

namespace NetCoreEventFlow.Api.Core.Events
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

