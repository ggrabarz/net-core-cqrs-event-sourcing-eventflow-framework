using EventFlow.Core;

namespace NetCoreEventFlow.Api.Elements
{
    public sealed class ExampleId : Identity<ExampleId>
    {
        public ExampleId(string value) : base(value) { }
    }
}
