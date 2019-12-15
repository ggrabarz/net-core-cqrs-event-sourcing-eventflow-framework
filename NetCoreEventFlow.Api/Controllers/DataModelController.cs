using EventFlow.Configuration;
using EventFlow.ReadStores;
using Microsoft.AspNetCore.Mvc;
using NetCoreEventFlow.Api.Core.ReadModel;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreEventFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataModelController : ControllerBase
    {
        private readonly IReadModelPopulator _readModelPopulator;

        public DataModelController(IResolver resolver)
        {
            _readModelPopulator = resolver.Resolve<IReadModelPopulator>();
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<ActionResult> ReplayEvents()
        {
            await _readModelPopulator.PopulateAsync<InventoryItemReadModel>(CancellationToken.None);
            return Accepted("Read models are replayed");
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        public async Task<ActionResult> DeleteEvents()
        {
            await _readModelPopulator.PurgeAsync<InventoryItemReadModel>(CancellationToken.None);
            return Ok("Read models deleted");
        }
    }
}
