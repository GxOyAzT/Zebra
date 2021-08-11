using System.Collections.Generic;
using System.Linq;

namespace Zebra.CustomerService.Application.Shared.ErrorCollector
{
    public class ErrorCollector : IErrorCollector
    {
        public ErrorCollector()
        {
            errors = new List<string>();
        }

        private List<string> errors;

        public List<string> GetErrors() => errors;

        public bool HasErrorOccured() => errors.Any();

        public void AddError(string message) => errors.Add(message);
    }
}
