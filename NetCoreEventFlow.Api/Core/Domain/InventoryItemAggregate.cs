using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using NetCoreEventFlow.Api.Core.Events;
using EventFlow.Snapshots;
using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Snapshots.Strategies;
using NetCoreEventFlow.Api.Core.Domain.Specifications;
using System.Linq;

namespace NetCoreEventFlow.Api.Core.Domain
{
    public class InventoryItemAggregate : SnapshotAggregateRoot<InventoryItemAggregate, InventoryItemId, InventoryItemSnapshot>, IEmit<InventoryItemCreatedEvent>, IEmit<InventoryItemRenamedEvent>, 
        IEmit<ItemsRemovedFromInventoryEvent>, IEmit<ItemsCheckedInToInventoryEvent>, IEmit<InventoryItemDeactivatedEvent>
    {
        private bool _activated; // custom business logic field
        private string _name;
        private int _count;
        private bool _inited; // added in order to prevent using init method twice

        public InventoryItemAggregate(InventoryItemId id) : base(id, SnapshotEveryFewVersionsStrategy.With(50)) { }

        public IExecutionResult Init(string name)
        {
            if (_inited) throw new InvalidOperationException("already inited object");
            Emit(new InventoryItemCreatedEvent(name));
            return ExecutionResult.Success();
        }

        public void Apply(InventoryItemCreatedEvent e)
        {
            _inited = true;
            _activated = true;
            _name = e.Name;
            _count = 0;
        }

        public IExecutionResult ChangeName(string newName)
        {
            if (string.IsNullOrEmpty(newName)) throw new ArgumentException("newName");
            Emit(new InventoryItemRenamedEvent(newName));
            return ExecutionResult.Success();
        }

        public void Apply(InventoryItemRenamedEvent e)
        {
            _name = e.NewName;
        }

        public IExecutionResult Remove(int count)
        {
            if (count <= 0) throw new InvalidOperationException("cant remove negative count from inventory");
            Emit(new ItemsRemovedFromInventoryEvent(count));
            return ExecutionResult.Success();
        }

        public void Apply(ItemsRemovedFromInventoryEvent e)
        {
            _count -= e.Count;
        }

        public IExecutionResult CheckIn(int count)
        {
            var errors = new CheckInNumberSpecification().WhyIsNotSatisfiedBy(count);
            if (errors.Any())
            {
                return ExecutionResult.Failed(errors);
            }
            Emit(new ItemsCheckedInToInventoryEvent(count));
            return ExecutionResult.Success();
        }

        public void Apply(ItemsCheckedInToInventoryEvent e)
        {
            _count += e.Count;
        }

        public IExecutionResult Deactivate()
        {
            if (!_activated) throw new InvalidOperationException("already deactivated");
            Emit(new InventoryItemDeactivatedEvent());
            return ExecutionResult.Success();
        }

        public void Apply(InventoryItemDeactivatedEvent e)
        {
            _activated = false;
        }

        protected override Task<InventoryItemSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(new InventoryItemSnapshot()
            {
                Activated = _activated,
                Name = _name,
                Count = _count,
                Inited = _inited,
            });
        }

        protected override Task LoadSnapshotAsync(InventoryItemSnapshot snapshot, ISnapshotMetadata metadata, CancellationToken cancellationToken)
        {
            _activated = snapshot.Activated;
            _name = snapshot.Name;
            _count = snapshot.Count;
            _inited = snapshot.Inited;
            return Task.CompletedTask;
        }
    }
}
