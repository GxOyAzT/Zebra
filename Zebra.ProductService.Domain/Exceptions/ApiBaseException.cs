using System;
using System.Net;

namespace Zebra.ProductService.Domain.Exceptions
{
    public class ApiBaseException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public ApiBaseException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public ApiBaseException()
        {
        }
    }
}
