using System;
using Zebra.Shared.LoggerDriver.Domain.Enums;

namespace Zebra.Shared.LoggerDriver.Domain.Models
{
    public class LogModel
    {
        public Guid Id { get; set; }
        public LogTypeEnum LogType { get; set; }
        public string Sender { get; set; }
        public DateTime Time { get; private set; }
        public string Message { get; set; }

        public LogModel()
        {
            Time = DateTime.Now;
        }
    }
}
