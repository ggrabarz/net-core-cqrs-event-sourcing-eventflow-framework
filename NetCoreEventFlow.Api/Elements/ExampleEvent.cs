using EventFlow.Aggregates;
using EventFlow.EventStores;

namespace NetCoreEventFlow.Api.Elements
{
    [EventVersion("example", 1)]
    public sealed class ExampleEvent : AggregateEvent<ExampleAggregate, ExampleId>
    {
        public ExampleEvent(int magicNumber)
        {
            MagicNumber = magicNumber;
        }

        public int MagicNumber { get; }
    }
}
