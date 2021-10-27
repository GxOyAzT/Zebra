﻿using Moq;
using System;
using System.Threading;
using Xunit;
using Zebra.ProductService.Application.Features.Files;
using Zebra.Shared.FileDriver.Features.Save;

namespace Zebra.ProductService.Application.Tests.Features.Files
{
    public class SaveFileCommandHandlerTests
    {
        //[Fact]
        //public void TestA()
        //{
        //    var mockSaveFile = new Mock<ISaveFile>();

        //    mockSaveFile.Setup(m => m.Save(It.IsAny<byte[]>(), It.IsAny<string>(), It.IsAny<string>()))
        //        .Returns("");

        //    var bytes = Convert.FromBase64String("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");

        //    new SaveFileCommandHandler(mockSaveFile.Object).Handle(new SaveFileCommand("data:image/png;base64,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", "", "filea"), new CancellationToken());

        //    mockSaveFile.Verify(m => m.Save(It.Is<byte[]>(p => p.Equals(bytes)), It.Is<string>(p => p == ""), It.Is<string>(p => p == "filea.png")));
        //}
    }
}
