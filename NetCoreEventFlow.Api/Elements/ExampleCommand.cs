using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace NetCoreEventFlow.Api.Elements
{
    [CommandVersion("example", 1)]
    public sealed class ExampleCommand : Command<ExampleAggregate, ExampleId, IExecutionResult>
    {
        public ExampleCommand(ExampleId aggregateId, int magicNumber) : base(aggregateId)
        {
            MagicNumber = magicNumber;
        }

        public int MagicNumber { get; }
    }
}
