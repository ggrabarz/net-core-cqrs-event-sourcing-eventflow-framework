using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates;
using EventFlow.Subscribers;
using NetCoreEventFlow.Domain.Inventory;
using NetCoreEventFlow.Domain.Inventory.Events;

namespace NetCoreEventFlow.Application.DomainEventHandlers
{
    public class InventoryItemRenamedEventSubscriber : ISubscribeSynchronousTo<InventoryItemAggregate, InventoryItemId, InventoryItemRenamedEvent>
    {
        public Task HandleAsync(IDomainEvent<InventoryItemAggregate, InventoryItemId, InventoryItemRenamedEvent> domainEvent, CancellationToken cancellationToken)
        {
            _ = domainEvent.AggregateEvent.NewName; // this will be executed when create event will be published and finish within PublishAsync
            return Task.CompletedTask;
        }
    }
}
