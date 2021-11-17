using System.Net;

namespace Zebra.ProductService.Domain.Exceptions
{
    public class DomainRulesException : ApiBaseException
    {
        public DomainRulesException(string message, HttpStatusCode statusCode)
            : base(message, statusCode)
        {
        }

        public DomainRulesException()
        {
        }
    }
}
