namespace Zebra.Shared.LoggerDriver.RabbitMqConfiguration
{
    public class SenderName
    {
        public SenderName(string senderName)
        {
            _SenderName = senderName;
        }

        public string _SenderName { get; }
    }
}
