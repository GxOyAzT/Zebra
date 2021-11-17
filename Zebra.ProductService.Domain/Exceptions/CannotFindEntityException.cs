using System.Net;

namespace Zebra.ProductService.Domain.Exceptions
{
    public class CannotFindEntityException : ApiBaseException
    {
        public CannotFindEntityException(string message, HttpStatusCode statusCode)
            :base(message, statusCode)
        {
        }

        public CannotFindEntityException()
        {
        }
    }
}
