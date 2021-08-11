using System;

namespace Zebra.CustomerService.Domain.Exceptions
{
    public class IncorrectInputException : Exception
    {
        public IncorrectInputException(string message)
            :base(message)
        {
        }

        public IncorrectInputException()
        {
        }
    }
}
