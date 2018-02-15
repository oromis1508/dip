using demo.framework.BaseEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTesting = Microsoft.VisualStudio.TestTools.UnitTesting;

namespace demo.framework.Utils
{
    public class Asserts : BaseEntity
    {
        private static bool _isSoftAssert;

        private Asserts(bool isSoftAssert)
        {
            _isSoftAssert = isSoftAssert;
        }

        public void AreEqual(object expected, object actual, string message)
        {
            try
            {
                UnitTesting.Assert.AreEqual(expected, actual, message);
                Log.Info($"== {message} == SUCCESSFULLY ==");
            }
            catch (AssertFailedException)
            {
                Log.Fatal($"== {message} == UNSUCCESSFULLY ==");
                Log.Fatal($"== expected: {expected} == actual: {actual} ==");

                if (!_isSoftAssert)
                    throw new AssertFailedException();
            }
        }

        public void IsTrue(bool expression, string message)
        {
            try
            {
                UnitTesting.Assert.IsTrue(expression, message);
                Log.Info($"== {message} == SUCCESSFULLY ==");
            }
            catch (AssertFailedException)
            {
                Log.Fatal($"== {message} == UNSUCCESSFULLY ==");

                if (!_isSoftAssert)
                    throw new AssertFailedException();
            }
        }

        public void IsFalse(bool expression, string message)
        {
            try
            {
                UnitTesting.Assert.IsFalse(expression, message);
                Log.Info($"== {message} == SUCCESSFULLY ==");
            }
            catch (AssertFailedException)
            {
                Log.Fatal($"== {message} == UNSUCCESSFULLY ==");
                if (!_isSoftAssert)
                    throw new AssertFailedException();
            }
        }

        public static Asserts Assert => new Asserts(false);

        public static Asserts SoftAssert => new Asserts(true);
    }
}
