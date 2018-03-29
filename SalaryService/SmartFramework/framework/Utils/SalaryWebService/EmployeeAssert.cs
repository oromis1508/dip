using SalaryWebServiceEntities.Entities;

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
            Asserts.AreEqual(expected.Id, actual.Id, $"{message} (Id)", isSoftAssert);
            Asserts.AreEqual(expected.PrivateId, actual.PrivateId, $"{message} (PrivateId)", isSoftAssert);
            Asserts.AreEqual(expected.FirstName, actual.FirstName, $"{message} (FirstName)", isSoftAssert);
            Asserts.AreEqual(expected.LastName, actual.LastName, $"{message} (LastName)", isSoftAssert);
            Asserts.AreEqual(expected.MiddleName, actual.MiddleName, $"{message} (MiddleName)", isSoftAssert);
            Asserts.AreEqual(expected.Experiense, actual.Experiense, $"{message} (Experiense)", isSoftAssert);
            Asserts.AreEqual(expected.ProfessionId, actual.ProfessionId, $"{message} (ProfessionId)", isSoftAssert);
        }
    }
}
