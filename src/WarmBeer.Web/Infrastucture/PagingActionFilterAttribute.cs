using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WarmBeer.Web.Models;

namespace WarmBeer.Web.Infrastucture
{
    /// <summary>
    /// http://aspnetwebstack.codeplex.com/workitem/1944
    /// </summary>
    public class PagingActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ActionArguments["paging"] == null)
            {
                actionContext.ActionArguments["paging"] = new PagingModel();
            }
        }
    }
}