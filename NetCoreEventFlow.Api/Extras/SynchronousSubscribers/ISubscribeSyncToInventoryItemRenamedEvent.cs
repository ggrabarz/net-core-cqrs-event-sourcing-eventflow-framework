using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates;
using EventFlow.Subscribers;
using NetCoreEventFlow.Api.Core.Domain;
using NetCoreEventFlow.Api.Core.Events;

namespace NetCoreEventFlow.Api.Extras.SynchronousSubscribers
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
