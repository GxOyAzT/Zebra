using System.Net;

namespace Zebra.ProductService.Domain.Exceptions
{
    public class IncorrectInputFormatException : ApiBaseException
    {
        public IncorrectInputFormatException(string message, HttpStatusCode statusCode) 
            : base(message, statusCode)
        {
        }

        public IncorrectInputFormatException()
        {
        }
    }
}
