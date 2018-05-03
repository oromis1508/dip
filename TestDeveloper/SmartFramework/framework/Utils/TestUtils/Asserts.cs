using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smart.framework.BaseEntities;

namespace smart.framework.Utils.TestUtils
{
    public class Asserts : BaseEntity
    {
        private static bool _isSoftAssertCaused;

        public static void AreEqual(object expected, object actual, string message, bool isSoftAssert = false)
        {
            try
            {
                Assert.AreEqual(expected, actual, message);
                Log.Info($"== {message} == SUCCESSFULLY ==");
            }
            catch (AssertFailedException)
            {
                _isSoftAssertCaused = true;
                Log.Fatal($"== {message} == UNSUCCESSFULLY ==");
                Log.Fatal($"== expected: {expected} == actual: {actual} ==");

                if (!isSoftAssert)
                    throw new AssertFailedException(message);
            }
        }

        public static void AreNotEqual(object expected, object actual, string message, bool isSoftAssert = false)
        {
            try
            {
                Assert.AreNotEqual(expected, actual, message);
                Log.Info($"== {message} == SUCCESSFULLY ==");
            }
            catch (AssertFailedException)
            {
                _isSoftAssertCaused = true;
                Log.Fatal($"== {message} == UNSUCCESSFULLY ==");

                if (!isSoftAssert)
                    throw new AssertFailedException(message);
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
                _isSoftAssertCaused = true;
                Log.Fatal($"== {message} == UNSUCCESSFULLY ==");

                if (!isSoftAssert)
                    throw new AssertFailedException(message);
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
                _isSoftAssertCaused = true;
                Log.Fatal($"== {message} == UNSUCCESSFULLY ==");
                if (!isSoftAssert)
                    throw new AssertFailedException(message);
            }
        }

        public static void IsListContains<T>(List<T> source, List<T> values, string message, bool isSoftAssert = false)
        {
            foreach (var value in values)
            {
                IsTrue(source.Contains(value), $"List contains {value}", true);
            }

            if (!isSoftAssert && _isSoftAssertCaused)
                throw new AssertFailedException(message);
        }

        public static void RaiseSoftAssertExeption()
        {
            if (_isSoftAssertCaused)
            {
                _isSoftAssertCaused = false;
                throw new AssertFailedException("Soft assert(-s) was caused");
            }
        }
    }
}
