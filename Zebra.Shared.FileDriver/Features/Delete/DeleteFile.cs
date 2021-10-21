using System.IO;

namespace Zebra.Shared.FileDriver.Features.Delete
{
    public class DeleteFile : IDeleteFile
    {
        public void Delete(string fullPath)
        {
            File.Delete(fullPath);
        }
    }
}
