using System;
using System.Drawing;
using demo.framework.BaseEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace demo.framework.Utils
{
    internal class BitmapUtil : BaseEntity
    {
        private const int DesiredProcentSamePixels = 90;

        public static bool ArePixelsEqual(Bitmap expected, Bitmap actual, string message)
        {
            var expectedPixelNum = expected.Width * expected.Height;
            var actualPixelNum = actual.Width * actual.Height;
            var samePixels = 0;

            if (expectedPixelNum != actualPixelNum)
            {
                throw new AssertFailedException();
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
            if (procentSamePixels < DesiredProcentSamePixels)
            {
                Log.Fatal($"== {message} == UNSUCCESSFULLY ==");
                Log.Fatal($"== procent of same pixels: {procentSamePixels} == but need: {DesiredProcentSamePixels} ==");
                return false;
            }

            return true;
        }
    }
}
