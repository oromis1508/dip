using demo.framework.Utils.SalaryWebService.Entities;

namespace demo.framework.Utils.SalaryWebService
{
    public class EmployeeAssert
    {
        public static void AreEqual(Employee expected, Employee actual, string message, bool isSoftAssert = false)
        {
            if (expected.FirstName.Equals(actual.LastName))
            {
                var buffer = expected.FirstName;
                expected.FirstName = expected.LastName;
                expected.LastName = buffer;
            }
            Asserts.AreEqual(expected, actual, message, isSoftAssert);
        }
    }
}
