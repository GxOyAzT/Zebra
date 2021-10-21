using Microsoft.AspNetCore.Http;

namespace Zebra.Shared.FileDriver.Features.Save
{
    public interface ISaveFile
    {
        string Save(byte[] bytes, string directPath, string fileName);
    }
}
