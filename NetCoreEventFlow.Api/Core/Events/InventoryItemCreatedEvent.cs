using EventFlow.Aggregates;
using EventFlow.EventStores;
using NetCoreEventFlow.Api.Core.Domain;

namespace NetCoreEventFlow.Api.Core.Events
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

