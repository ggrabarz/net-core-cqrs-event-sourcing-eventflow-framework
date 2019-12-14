using EventFlow.Aggregates;
using EventFlow.ReadStores;
using NetCoreEventFlow.Api.Core.Domain;
using NetCoreEventFlow.Api.Core.Events;

namespace NetCoreEventFlow.Api.Core.ReadModel
{
    public sealed class InventoryItemReadModel : IReadModel, IAmReadModelFor<InventoryItemAggregate, InventoryItemId, InventoryItemCreatedEvent>,
        IAmReadModelFor<InventoryItemAggregate, InventoryItemId, InventoryItemDeactivatedEvent>,
        IAmReadModelFor<InventoryItemAggregate, InventoryItemId, InventoryItemRenamedEvent>
    {
        public string Id { get; private set; }
        public string Name { get; private set; }

        public void Apply(IReadModelContext context, IDomainEvent<InventoryItemAggregate, InventoryItemId, InventoryItemRenamedEvent> domainEvent)
        {
            Name = domainEvent.AggregateEvent.NewName;
        }

        public void Apply(IReadModelContext context, IDomainEvent<InventoryItemAggregate, InventoryItemId, InventoryItemDeactivatedEvent> domainEvent)
        {
            context.MarkForDeletion();
        }

        public void Apply(IReadModelContext context, IDomainEvent<InventoryItemAggregate, InventoryItemId, InventoryItemCreatedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.ToString();
            Name = domainEvent.AggregateEvent.Name;
        }
    }
}
