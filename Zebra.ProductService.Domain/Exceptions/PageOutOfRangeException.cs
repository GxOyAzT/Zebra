using System.Net;

namespace Zebra.ProductService.Domain.Exceptions
{
    public class PageOutOfRangeException : ApiBaseException
    {
        public PageOutOfRangeException(string message, HttpStatusCode statusCode) 
            : base(message, statusCode)
        {
        }

        public PageOutOfRangeException()
        {
        }
    }
}
