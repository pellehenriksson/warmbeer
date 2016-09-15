using System.Threading.Tasks;
using System.Web.Http;
using MediatR;
using WarmBeer.Core.Queries.Location;

namespace WarmBeer.Web.Controllers
{
    [RoutePrefix("api/location")]
    public class LocationController : ApiController
    {
        private readonly IMediator mediator;

        public LocationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Route("name")]
        public async Task<IHttpActionResult> Get(decimal longitude, decimal latitude)
        {
            var parameters = new LocationNameQuery.Parameters(longitude, latitude);
            var result = await this.mediator.SendAsync(parameters);

            return this.Ok(result);
        }
    }
}
