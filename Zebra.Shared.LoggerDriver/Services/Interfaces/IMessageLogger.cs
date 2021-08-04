using Zebra.Shared.LoggerDriver.Domain.Enums;

namespace Zebra.Shared.LoggerDriver.Services.Interfaces
{
    public interface IMessageLogger
    {
        void Log(string message, LogTypeEnum logType);
    }
}
