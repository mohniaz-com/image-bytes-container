using System;
using System.IO;
using System.Text;
using SkiaSharp;

namespace ImageBytesContainer;

// ReSharper disable once InconsistentNaming
public static class IBCEncoder
{
    public static void EncodeToImage(string text, string fileDestinationPath)
    {
        var bytes = Encoding.UTF8.GetBytes(text);
        var size = CalculateSize(bytes.Length);
        var bitmap = new SKBitmap(size.width, size.height);

        if (!bitmap.ReadyToDraw)
            throw new ApplicationException("Cannot draw on bitmap.");

        for (var byteIndex = 0; byteIndex < bytes.Length; byteIndex++)
        {
            var color = new SKColor(bytes[byteIndex], 0, 0);
            var x = byteIndex % size.width;
            var y = byteIndex / size.width;
            bitmap.SetPixel(x, y, color);
        }

        using var fileStream = File.OpenWrite(fileDestinationPath);
        var result = bitmap.Encode(fileStream, SKEncodedImageFormat.Png, 100);

        if (!result)
            throw new ApplicationException("Failed to encode to image file.");
    }

    public static string DecodeFromImage(string fileSourcePath)
    {
        using var fileStream = File.OpenRead(fileSourcePath);
        var bitmap = SKBitmap.Decode(fileStream);
        
        if (bitmap == null)
            throw new ApplicationException("Failed to decode to image file.");
        
        var bytes = new byte[bitmap.Pixels.Length];

        for (var pixelIndex = 0; pixelIndex < bytes.Length; pixelIndex++)
        {
            var color = bitmap.GetPixel(pixelIndex, 0);
            if (color.Alpha == 0) // Reached end of bytes
                break;
            bytes[pixelIndex] = color.Red;
        }

        var outputString = Encoding.UTF8.GetString(bytes);
        return outputString;
    }

    private static (int width, int height) CalculateSize(int numberOfBytes)
    {
        // Find square side length
        var width = (int)Math.Ceiling(Math.Sqrt(numberOfBytes));
        return (width, width);
    }
}