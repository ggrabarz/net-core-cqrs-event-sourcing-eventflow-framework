using EventFlow.Aggregates;
using EventFlow.EventStores;
using NetCoreEventFlow.Api.Core.Domain;

namespace NetCoreEventFlow.Api.Core.Events
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

