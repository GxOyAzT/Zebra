using System;

namespace Zebra.ProductService.Domain.Exceptions
{
    public class PageOutOfRangeException : Exception
    {
        public PageOutOfRangeException()
        {
        }

        public PageOutOfRangeException(string message) : base(message)
        {
        }
    }
}
