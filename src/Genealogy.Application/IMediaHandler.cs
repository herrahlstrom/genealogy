using System;
using System.Drawing;
using System.Linq;

namespace Genealogy.Application;

public interface IMediaHandler
{
    public Task ResizeImage(string source, string target, Size maxSize);
}
