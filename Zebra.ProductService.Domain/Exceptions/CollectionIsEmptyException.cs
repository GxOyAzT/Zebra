using System;

namespace Zebra.ProductService.Domain.Exceptions
{
    public class CollectionIsEmptyException : Exception
    {
        public CollectionIsEmptyException(string message)
            :base(message)
        {
        }

        public CollectionIsEmptyException()
        {
        }
    }
}
