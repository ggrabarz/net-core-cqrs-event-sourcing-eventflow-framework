﻿using EventFlow.Queries;
using EventFlow.ReadStores.InMemory;
using NetCoreEventFlow.Api.Core.ReadModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreEventFlow.Api.Extras.Queries
{
    public sealed class GetUserByUsernameQueryHandler : IQueryHandler<GetInventoryItemsListQuery, IEnumerable<GetInventoryItemsListResult>>
    {
        private IInMemoryReadStore<InventoryItemReadModel> _readModel;

        public GetUserByUsernameQueryHandler(IInMemoryReadStore<InventoryItemReadModel> readModel)
        {
            _readModel = readModel;
        }

        public async Task<IEnumerable<GetInventoryItemsListResult>> ExecuteQueryAsync(GetInventoryItemsListQuery query, CancellationToken cancellationToken)
        {
            var result = await _readModel.FindAsync(x => true, cancellationToken);
            return result.Select(x => new GetInventoryItemsListResult() { Id = x.Id, Name = x.Name });
        }
    }
}
