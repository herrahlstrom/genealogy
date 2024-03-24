using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.Versioning;
using Genealogy.Application;

namespace Genealogy.Infrastructure;

internal class MediaHandler : IMediaHandler
{
    [SupportedOSPlatform("windows")]
    public Task ResizeImage(string source, string target, Size maxSize)
    {
        Image image = Image.FromFile(source);
        var targetSize = GetTargetSize(new Size(image.Width, image.Height), maxSize);

        var destRect = new Rectangle(0, 0, targetSize.Width, targetSize.Height);
        var destImage = new Bitmap(destRect.Width, destRect.Height);
        destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

        using (var graphics = Graphics.FromImage(destImage))
        {
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            using var wrapMode = new ImageAttributes();
            wrapMode.SetWrapMode(WrapMode.TileFlipXY);
            graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
        }

        destImage.Save(target, image.RawFormat);

        return Task.CompletedTask;
    }

    private Size GetTargetSize(Size sourceSize, Size maxSize)
    {
        var nPercentW = (float)maxSize.Width / sourceSize.Width;
        var nPercentH = (float)maxSize.Height / sourceSize.Height;

        if (nPercentH < nPercentW)
        {
            return new((int)(sourceSize.Width * nPercentH), maxSize.Height);
        }
        else
        {
            return new(maxSize.Width, (int)(sourceSize.Height * nPercentW));
        }
    }
}
