using EventFlow.Core;

namespace NetCoreEventFlow.Domain.Inventory
{
    public sealed class InventoryItemId : Identity<InventoryItemId>
    {
        public InventoryItemId(string value) : base(value)
        {
        }
    }
}
