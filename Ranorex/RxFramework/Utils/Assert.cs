using System;
using System.Collections.Generic;
using System.Linq;

namespace RxFramework.Utils
{
    public static class Assert
    {
        private static List<Exception> _exceptions = new List<Exception>();

        public static void AreEqual(object expected, object actual, string condition)
        {
            if (expected.Equals(actual))
            {
                Logger.Instance.Info($"Assertion :: {condition} :: PASSED");
            }
            else
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, actual,
                    $"Assertion ::{condition}:: FALSE");
            }
        }

        public static void AreNotEqual(object expected, object actual, string condition)
        {
            if (!expected.Equals(actual))
            {
                Logger.Instance.Info($"Assertion :: {condition} :: PASSED");
            }
            else
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotEqual(expected, actual,
                    $"Assertion ::{condition}:: FALSE");
            }
        }

        public static void IsTrue(bool expression, string condition)
        {
            if (expression)
            {
                Logger.Instance.Info($"Assertion :: {condition} :: PASSED");
            }
            else
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(expression,
                    $"Assertion ::{condition}:: FALSE");
            }
        }

        public static void IsFalse(bool expression, string condition)
        {
            if (!expression)
            {
                Logger.Instance.Info($"Assertion :: {condition} :: PASSED");
            }
            else
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsFalse(expression,
                    $"Assertion ::{condition}:: FALSE");
            }
        }

        public static void Batch(params Action[] assertMethods)
        {
            var exceptions = ExceptionUtils.Catch(assertMethods);
            if (exceptions.Length == 0) return;
            var message = string.Join(Environment.NewLine, exceptions.Select(ex => ex.Message));
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail(Environment.NewLine + message);
        }

        public static void SoftAreEqual(object expected, object actual, string condition)
        {
            try
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, actual,
                    $"Assertion :: {condition} :: FALSE");
            }
            catch (Exception e)
            {
                _exceptions.Add(e);
            }
        }

        public static void SoftAssertBegin()
        {
            _exceptions = new List<Exception>();
        }

        public static void SoftAssertEnd()
        {
            if (_exceptions.Count > 0)
            {
                throw new AggregateException(_exceptions);
            }
        }
    }
}
