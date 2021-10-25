using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Zebra.Shared.FileDriver.Features.Save;

namespace Zebra.ProductService.Application.Features.Files
{
    public sealed record SaveFileCommand(string fileSrc, string path, string fileName) : IRequest<string>;

    public sealed class SaveFileCommandHandler : IRequestHandler<SaveFileCommand, string>
    {
        private readonly ISaveFile _saveFile;

        public SaveFileCommandHandler(ISaveFile saveFile)
        {
            _saveFile = saveFile;
        }

        public Task<string> Handle(SaveFileCommand request, CancellationToken cancellationToken)
        {
            var spitSrc = request.fileSrc.Split(";base64,");

            var spitHead = spitSrc[0].Split('/');

            return Task.FromResult(_saveFile.Save(Convert.FromBase64String(spitSrc[1]), request.path, $"{request.fileName}.{spitHead[1]}"));
        }
    }
}
