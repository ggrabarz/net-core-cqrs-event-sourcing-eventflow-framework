﻿using EventFlow.Aggregates;
using EventFlow.ReadStores;

namespace NetCoreEventFlow.Api.Elements
{
    public class ExampleReadModel : IReadModel, IAmReadModelFor<ExampleAggregate, ExampleId, ExampleEvent>
    {
        public int MagicNumber { get; private set; }

        public void Apply(IReadModelContext context, IDomainEvent<ExampleAggregate, ExampleId, ExampleEvent> domainEvent)
        {
            MagicNumber = domainEvent.AggregateEvent.MagicNumber;
        }
    }
}
