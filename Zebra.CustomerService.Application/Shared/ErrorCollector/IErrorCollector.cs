using System.Collections.Generic;

namespace Zebra.CustomerService.Application.Shared.ErrorCollector
{
    public interface IErrorCollector
    {
        List<string> GetErrors();
    }
}
