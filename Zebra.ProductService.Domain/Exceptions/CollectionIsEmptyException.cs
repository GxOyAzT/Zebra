using System.Net;

namespace Zebra.ProductService.Domain.Exceptions
{
    public class CollectionIsEmptyException : ApiBaseException
    {
        public CollectionIsEmptyException(string message, HttpStatusCode statusCode)
            :base(message, statusCode)
        {
        }

        public CollectionIsEmptyException()
        {
        }
    }
}
