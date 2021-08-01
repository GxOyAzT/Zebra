using System;

namespace Zebra.ProductService.Domain.Exceptions
{
    public class DomainRulesException : Exception
    {
        public DomainRulesException(string message)
            : base(message)
        {
        }

        public DomainRulesException()
        {
        }
    }
}
