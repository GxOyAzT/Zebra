using System.Collections.Generic;
using Xunit;
using Zebra.ProductService.Domain.ApiModels.Paged;
using Zebra.ProductService.Domain.Exceptions;

namespace Zebra.ProductService.Domain.Tests.ApiModels.Paged
{
    public class PagedListTests
    {
        List<string> mockList;
        public PagedListTests()
        {
            mockList = new List<string>();
            for(int i = 1; i <= 20; i++)
            {
                mockList.Add($"Item {i}");
            }
        }

        [Fact]
        public void TestA()
        {
            var pagedList = new PagedList<string>(mockList, 5, 2);

            Assert.Equal(2, pagedList.Page);
            Assert.Equal(5, pagedList.PageCapacity);
            Assert.Equal(4, pagedList.TotalPages);
            Assert.Equal(20, pagedList.TotalModels);
        }

        [Fact]
        public void TestB()
        {
            var pagedList = new PagedList<string>(mockList, 7, 2);

            Assert.Equal(3, pagedList.TotalPages);
        }

        [Fact]
        public void TestC()
        {
            Assert.Throws<PageOutOfRangeException>(() => new PagedList<string>(mockList, 5, 5));
        }

        [Fact]
        public void TestD()
        {
            var pagedList = new PagedList<string>(mockList, 6, 2);

            var models = pagedList.Models;

            Assert.Equal(6, models.Count);

            Assert.Contains("Item 7", models);
            Assert.Contains("Item 8", models);
            Assert.Contains("Item 9", models);
            Assert.Contains("Item 10", models);
            Assert.Contains("Item 11", models);
            Assert.Contains("Item 12", models);
        }

        [Fact]
        public void TestE()
        {
            var pagedList = new PagedList<string>(new List<string>(), 6, 1);

            var models = pagedList.Models;

            Assert.Empty(models);
        }

        [Fact]
        public void TestF()
        {
            var pagedList = new PagedList<string>(mockList, 6, 4);

            var models = pagedList.Models;

            Assert.Equal(2, models.Count);

            Assert.Contains("Item 19", models);
            Assert.Contains("Item 20", models);
        }
    }
}
