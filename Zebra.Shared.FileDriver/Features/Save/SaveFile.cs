using System.IO;
using Zebra.Shared.FileDriver.Configuration;

namespace Zebra.Shared.FileDriver.Features.Save
{
    public class SaveFile : ISaveFile
    {
        public string RootPath { get; }

        public SaveFile(Options opt)
        {
            RootPath = opt.RootPath;
        }

        public string Save(byte[] bytes, string directPath, string fileName)
        {
            var fullPath = Path.Combine(RootPath, directPath, $"{fileName}");

            File.WriteAllBytes(fullPath, bytes);

            return fullPath;
        }
    }
}
