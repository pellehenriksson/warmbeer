using System.Threading.Tasks;
using System.Web.Http;
using MediatR;
using WarmBeer.Core.Queries.Items;
using WarmBeer.Web.Infrastucture;
using WarmBeer.Web.Models;

namespace WarmBeer.Web.Controllers
{
    [RoutePrefix("api/items")]
    public class IndexController : ApiController
    {
        private readonly IMediator mediator;

        public IndexController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
        [PagingActionFilter]
        [Route("")]
        public async Task<IHttpActionResult> Get([FromUri] PagingModel paging)
        {
            var critera = new ItemsListQuery.Parameters { Page = paging.Page, Size = paging.Size };
            var result = await this.mediator.SendAsync(critera);

            return this.Ok(result);
        }
    }
}
