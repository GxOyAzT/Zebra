using System;
using Zebra.Logger.API.Domain.Enums;

namespace Zebra.Logger.API.Domain.Models
{
    public class LogModel
    {
        public Guid Id { get; set; }
        public LogTypeEnum LogType { get; set; }
        public string Sender { get; set; }
        public DateTime Time { get; set; }
        public string Message { get; set; }
    }
}
