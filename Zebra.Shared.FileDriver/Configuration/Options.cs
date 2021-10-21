namespace Zebra.Shared.FileDriver.Configuration
{
    public class Options
    {
        public string RootPath { get; set; }

        public Options(string rootPath)
        {
            RootPath = rootPath;
        }
    }
}
