using System.Threading.Tasks;
using System.Web.Http;
using MediatR;
using WarmBeer.Core.Queries.Items;
using WarmBeer.Web.Infrastucture;
using WarmBeer.Web.Models;

namespace WarmBeer.Web.Controllers
{
    [RoutePrefix("api/items")]
    public class ItemsController : ApiController
    {
        private readonly IMediator mediator;

        public ItemsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
        [PagingActionFilter]
        [Route("")]
        public async Task<IHttpActionResult> Get([FromUri] PagingModel paging)
        {
            var parameters = new ItemsListQuery.Parameters { Page = paging.Page, Size = paging.Size };
            var result = await this.mediator.SendAsync(parameters);

            return this.Ok(result);
        }

        [Route("id/{id:int}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var parameters = new ItemQuery.Parameters(id);
            var result = await mediator.SendAsync(parameters);

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }
    }
}
