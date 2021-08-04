using Zebra.Logger.API.Domain.Models;

namespace Zebra.Logger.API.Persistance.Interfaces
{
    public interface ILogsRepository
    {
        void Insert(LogModel logModel);
    }
}
