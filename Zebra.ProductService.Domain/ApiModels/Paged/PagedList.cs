using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Zebra.ProductService.Domain.Exceptions;

namespace Zebra.ProductService.Domain.ApiModels.Paged
{
    public class PagedList<TModel>
    {
        public PagedList(List<TModel> models, int pageCapacity, int page)
        {
            Page = page;
            PageCapacity = pageCapacity;
            _models = models ?? new List<TModel>();

            if (TotalModels != 0)
                if (TotalPages < Page)
                    throw new PageOutOfRangeException("Cannot create paged list of passed parameters", HttpStatusCode.BadRequest);
        }

        List<TModel> _models { get; }

        public List<TModel> Models 
        { 
            get
            {
                return _models.Skip((Page - 1) * PageCapacity).Take(PageCapacity).ToList();
            } 
        }

        public int Page { get; init; }
        public int PageCapacity { get; init; }
        public int TotalModels { get => _models.Count; }
        public int TotalPages 
        {
            get
            {
                return (int)Math.Ceiling(TotalModels * 1.0 / PageCapacity * 1.0);
            }
        }
    }
}
