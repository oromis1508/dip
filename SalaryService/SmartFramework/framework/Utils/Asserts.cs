using System.Drawing;
using demo.framework.BaseEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace demo.framework.Utils
{
    public class Asserts : BaseEntity
    {
        public static void AreEqual(object expected, object actual, string message, bool isSoftAssert = false)
        {
            try
            {
                if (expected is Bitmap)
                {
                    if (!BitmapUtil.ArePixelsEqual((Bitmap) expected, (Bitmap)actual, message))
                        return;
                }
                else
                {
                    Assert.AreEqual(expected, actual, message);
                }
                Log.Info($"== {message} == SUCCESSFULLY ==");
            }
            catch (AssertFailedException)
            {
                Log.Fatal($"== {message} == UNSUCCESSFULLY ==");
                Log.Fatal($"== expected: {expected} == actual: {actual} ==");

                if (!isSoftAssert)
                    throw new AssertFailedException();
            }
        }

        public static void IsTrue(bool expression, string message, bool isSoftAssert = false)
        {
            try
            {
                Assert.IsTrue(expression, message);
                Log.Info($"== {message} == SUCCESSFULLY ==");
            }
            catch (AssertFailedException)
            {
                Log.Fatal($"== {message} == UNSUCCESSFULLY ==");

                if (!isSoftAssert)
                    throw new AssertFailedException();
            }
        }

        public static void IsFalse(bool expression, string message, bool isSoftAssert = false)
        {
            try
            {
                Assert.IsFalse(expression, message);
                Log.Info($"== {message} == SUCCESSFULLY ==");
            }
            catch (AssertFailedException)
            {
                Log.Fatal($"== {message} == UNSUCCESSFULLY ==");
                if (!isSoftAssert)
                    throw new AssertFailedException();
            }
        }

        public static void IsNull(object expression, string message, bool isSoftAssert = false)
        {
            try
            {
                Assert.IsNull(expression, message);
                Log.Info($"== {message} == SUCCESSFULLY ==");
            }
            catch (AssertFailedException)
            {
                Log.Fatal($"== {message} == UNSUCCESSFULLY ==");
                if (!isSoftAssert)
                    throw new AssertFailedException();
            }
        }
    }
}
