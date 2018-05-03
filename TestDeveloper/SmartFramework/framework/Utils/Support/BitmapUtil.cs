using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using smart.framework.BaseEntities;

namespace smart.framework.Utils.Support
{
    internal class BitmapUtil : BaseEntity
    {
        public static bool ArePixelsEqual(Bitmap expected, Bitmap actual, string message, int desiredProcentSamePixels = 90)
        {
            var expectedPixelNum = expected.Width * expected.Height;
            var actualPixelNum = actual.Width * actual.Height;
            var samePixels = 0;

            if (expectedPixelNum != actualPixelNum)
            {
                Log.Fatal($"== {message} == UNSUCCESSFULLY ==");
                Log.Fatal("== dimensions of images is differ\n: ==" +
                          $"== expected width {expected.Width}, but actual width: {actual.Width} ==" +
                          $"== expected height {expected.Height}, but actual height: {actual.Height} ==");
                return false;
            }

            for (var x = 0; x < expected.Width; x++)
            {
                for (var y = 0; y < expected.Height; y++)
                {
                    var expPixel = expected.GetPixel(x, y);
                    var actPixel = actual.GetPixel(x, y);

                    if (expPixel.Equals(actPixel))
                    {
                        samePixels++;
                    }
                }
            }

            var procentSamePixels = Math.Round((double) samePixels / expectedPixelNum * 100, 2);
            if (procentSamePixels < desiredProcentSamePixels)
            {
                Log.Fatal($"== {message} == UNSUCCESSFULLY ==");
                Log.Fatal($"== procent of same pixels: {procentSamePixels} == but need: {desiredProcentSamePixels} ==");
                return false;
            }

            return true;
        }

        public static Bitmap TakeScreen()
        {
            ;
            var bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            var graphics = Graphics.FromImage(bitmap);
            graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
            return bitmap;
        }

        public static byte[] GetBytesFromImage(Bitmap image)
        {
            using (var stream = new MemoryStream())
            {
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
