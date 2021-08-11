using System.Collections.Generic;

namespace Zebra.CustomerService.Application.Shared.ErrorCollector
{
    public interface IErrorCollector
    {
        bool HasErrorOccured();
        List<string> GetErrors();
        void AddError(string message);
    }
}
