namespace Zebra.Shared.LoggerDriver.RabbitMqConfiguration
{
    public class Settings
    {
        public Settings(string senderName, bool isProduction = true)
        {
            _SenderName = senderName;
            _IsProduction = isProduction;
        }

        public string _SenderName { get; }

        public readonly bool _IsProduction;
    }
}
