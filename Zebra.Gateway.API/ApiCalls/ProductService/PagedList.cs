using System.Collections.Generic;

namespace Zebra.ApiCalls.ProductService
{
    public class PagedList<TModel>
    {
        public List<TModel> Models { get; set; }
        public int Page { get; set; }
        public int PageCapacity { get; set; }
        public int TotalModels { get;  set; }
        public int TotalPages { get; set; }
    }
}
