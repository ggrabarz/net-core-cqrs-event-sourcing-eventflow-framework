using EventFlow.Aggregates;
using EventFlow.ReadStores;
using NetCoreEventFlow.Domain.Inventory;
using NetCoreEventFlow.Domain.Inventory.Events;

namespace NetCoreEventFlow.ReadModel.DomainEventHandlers.Inventory
{
    public sealed class InventoryItemDetailsReadModel : IReadModel, IAmReadModelFor<InventoryItemAggregate, InventoryItemId, InventoryItemCreatedEvent>,
        IAmReadModelFor<InventoryItemAggregate, InventoryItemId, InventoryItemDeactivatedEvent>,
        IAmReadModelFor<InventoryItemAggregate, InventoryItemId, InventoryItemRenamedEvent>,
        IAmReadModelFor<InventoryItemAggregate, InventoryItemId, ItemsCheckedInToInventoryEvent>,
        IAmReadModelFor<InventoryItemAggregate, InventoryItemId, ItemsRemovedFromInventoryEvent>
    {
        public string Id { get; private set; }
        public bool Activated { get; private set; }
        public string Name { get; private set; }
        public int Count { get; private set; }

        public void Apply(IReadModelContext context, IDomainEvent<InventoryItemAggregate, InventoryItemId, InventoryItemRenamedEvent> domainEvent)
        {
            Name = domainEvent.AggregateEvent.NewName;
        }

        public void Apply(IReadModelContext context, IDomainEvent<InventoryItemAggregate, InventoryItemId, InventoryItemDeactivatedEvent> domainEvent)
        {
            context.MarkForDeletion();
        }

        public void Apply(IReadModelContext context, IDomainEvent<InventoryItemAggregate, InventoryItemId, ItemsCheckedInToInventoryEvent> domainEvent)
        {
            Count += domainEvent.AggregateEvent.Count;
        }

        public void Apply(IReadModelContext context, IDomainEvent<InventoryItemAggregate, InventoryItemId, ItemsRemovedFromInventoryEvent> domainEvent)
        {
            Count -= domainEvent.AggregateEvent.Count;
        }

        public void Apply(IReadModelContext context, IDomainEvent<InventoryItemAggregate, InventoryItemId, InventoryItemCreatedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.ToString();
            Name = domainEvent.AggregateEvent.Name;
            Count = 0;
            Activated = true;
        }
    }
}
