using System;

namespace Zebra.ProductService.Domain.Exceptions
{
    public class IncorrectInputFormatException : Exception
    {
        public IncorrectInputFormatException(string message) : base(message)
        {
        }

        public IncorrectInputFormatException()
        {
        }
    }
}
