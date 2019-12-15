using EventFlow.Aggregates;
using EventFlow.EventStores;
using NetCoreEventFlow.Api.Core.Domain;

namespace NetCoreEventFlow.Api.Core.Events
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

