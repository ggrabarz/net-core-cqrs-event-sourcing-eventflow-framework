using EventFlow.Queries;
using NetCoreEventFlow.ReadModel.Queries.Inventory.ViewModels;
using System.Collections.Generic;

namespace NetCoreEventFlow.ReadModel.Queries.Inventory
{
    public sealed class GetInventoryItemsListQuery : IQuery<IEnumerable<GetInventoryItemsListResult>>
    {
    }
}
