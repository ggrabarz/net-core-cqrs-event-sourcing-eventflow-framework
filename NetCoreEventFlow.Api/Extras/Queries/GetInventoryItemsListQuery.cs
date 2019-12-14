using EventFlow.Queries;
using System.Collections.Generic;

namespace NetCoreEventFlow.Api.Extras.Queries
{
    public sealed class GetInventoryItemsListQuery : IQuery<IEnumerable<GetInventoryItemsListResult>>
    {
    }
}
