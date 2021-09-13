using System.Collections.Generic;
using System.Linq;

namespace Zebra.CustomerService.Application.Shared.ErrorCollector
{
    public abstract class ErrorCollector
    {
        public ErrorCollector()
        {
            errors = new List<string>();
        }

        private List<string> errors;

        public List<string> GetErrors() => errors;

        public bool HasErrorOccured() => errors.Any();

        protected void AddError(string message) => errors.Add(message);
    }
}
