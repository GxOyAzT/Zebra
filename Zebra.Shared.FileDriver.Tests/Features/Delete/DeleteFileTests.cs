using Xunit;
using Zebra.Shared.FileDriver.Features.Delete;

namespace Zebra.Shared.FileDriver.Tests.Features.Delete
{
    public class DeleteFileTests
    {
        [Fact]
        public void DeleteTestA()
        {
            new DeleteFile().Delete(@"C:\Data\ZebraFiles\hello.png");
        }
    }
}
