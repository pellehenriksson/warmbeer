using System;
using System.Collections.Generic;

namespace WarmBeer.Core.Queries
{
    public abstract class PagedResult<T>
    {
        protected PagedResult(PagedParameters parameters, int totalCount, List<T> items)
        {
            this.Items = items;

            this.PageInfo = new PageInfo
            {
                TotalCount = totalCount,
                PageCount = ResolvePageCount(parameters, totalCount),
                ItemCount = this.Items.Count,
                CurrentPage = parameters.Page,
                PageSize = parameters.Size
            };
        }
        
        public List<T> Items { get; private set; }

        public PageInfo PageInfo { get; private set; }

        private static int ResolvePageCount(PagedParameters parameters, int totalCount)
        {
            var result = (int)Math.Ceiling((decimal)totalCount / parameters.Size);
            return result == 0 ? 1 : result;
        }
    }
}
