using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zebra.Shared.FileDriver.Features.Delete
{
    public interface IDeleteFile
    {
        void Delete(string fullPath);
    }
}
