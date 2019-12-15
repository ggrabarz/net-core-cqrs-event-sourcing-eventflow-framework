using EventFlow.Aggregates;
using EventFlow.EventStores;
using NetCoreEventFlow.Api.Core.Domain;

namespace NetCoreEventFlow.Api.Core.Events
{
    [EventVersion("InventoryItemDeactivated", 1)]
    public sealed class InventoryItemDeactivatedEvent : AggregateEvent<InventoryItemAggregate, InventoryItemId>
    {
    }
}

