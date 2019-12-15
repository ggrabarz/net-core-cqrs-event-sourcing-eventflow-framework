using EventFlow.Core;

namespace NetCoreEventFlow.Api.Core.Domain
{
    public sealed class InventoryItemId : Identity<InventoryItemId>
    {
        public InventoryItemId(string value) : base(value)
        {
        }
    }
}
