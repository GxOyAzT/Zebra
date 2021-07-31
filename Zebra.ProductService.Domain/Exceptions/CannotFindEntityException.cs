using System;

namespace Zebra.ProductService.Domain.Exceptions
{
    public class CannotFindEntityException : Exception
    {
        public CannotFindEntityException(string message)
            :base(message)
        {
        }

        public CannotFindEntityException()
        {
        }
    }
}
