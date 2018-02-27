using demo.framework.Utils.SalaryWebService.Entities;

namespace SalaryWebServiceEntities.Entities
{
    public class EmployeeSearchCriteria
    {
        private readonly string _criteria;

        private EmployeeSearchCriteria(string criteria)
        {
            _criteria = criteria;
        }

        public static EmployeeSearchCriteria ById(Employee employee) => new EmployeeSearchCriteria($"id={employee.Id}");

        public static EmployeeSearchCriteria ByPrivateId(Employee employee) => new EmployeeSearchCriteria($"private_id={employee.PrivateId}");

        public static EmployeeSearchCriteria ByFirstName(Employee employee) => new EmployeeSearchCriteria($"first_name={employee.FirstName}");

        public static EmployeeSearchCriteria ByLastName(Employee employee) => new EmployeeSearchCriteria($"last_name={employee.LastName}");

        public static EmployeeSearchCriteria ByMiddleName(Employee employee) => new EmployeeSearchCriteria($"middle_name={employee.MiddleName}");

        public static EmployeeSearchCriteria ByExperiense(Employee employee) => new EmployeeSearchCriteria($"exp={employee.Experiense}");

        public static EmployeeSearchCriteria ByProfessionId(Employee employee) => new EmployeeSearchCriteria($"profession_id={employee.ProfessionId}");

        public override string ToString() => _criteria;

    }
}
