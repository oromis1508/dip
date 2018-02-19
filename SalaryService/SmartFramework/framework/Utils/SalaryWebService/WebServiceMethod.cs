using demo.framework.Utils.Entities;

namespace demo.framework.Utils.SalaryWebService
{
    public class WebServiceMethod
    {
        private WebServiceMethod(string name, string body)
        {
            Name = name;
            Body = body;
        }

        public string Name { get;}
        public string Body { get;}

        public static WebServiceMethod AddEmployee(Employee employee)
        {
            var body =  $"      <newID>{employee.Id}</newID>\r\n" +
                        $"      <newPrivate_id>{employee.PrivateId}</newPrivate_id>\r\n" +
                        $"      <newFirst_name>{employee.FirstName}</newFirst_name>\r\n" +
                        $"      <newLast_name>{employee.LastName}</newLast_name>\r\n" +
                        $"      <newMiddle_name>{employee.MiddleName}</newMiddle_name>\r\n" +
                        $"      <newExp>{employee.Experiense}</newExp>\r\n" +
                        $"      <newProfession_id>{employee.ProfessionId}</newProfession_id>\r\n";
            return new WebServiceMethod("AddNewEmployee", body);
        }

        public static WebServiceMethod GetEmployeeByExperiense(Employee employee)
        {
            Body = $"      <exp>{employee.Experiense}</exp>\r\n" +
                   $"      <prof>{employee.ProfessionId}</prof>\r\n";
            return new WebServiceMethod("GetEmpByExpProf");
        }

        public static WebServiceMethod GetEmployeeByLastName(Employee employee)
        {
            Body = $"      <ln>{employee.LastName}</ln>\r\n";
            return new WebServiceMethod("GetEmpByLN");
        }

        public static WebServiceMethod GetEmployeeByPrivateId(Employee employee)
        {
            Body = $"      <pi>{employee.PrivateId}</pi>\r\n";
            return new WebServiceMethod("GetEmpByPI");
        }

        public static WebServiceMethod GetEmployeeSalary(Employee employee)
        {
            Body = $"      <pi>{employee.PrivateId}</pi>\r\n" +
                   $"      <workDays>{employee.SalaryData.WorkDays}</workDays>\r\n" +
                   $"      <sickDays>{employee.SalaryData.SickDays}</sickDays>\r\n" +
                   $"      <overDays>{employee.SalaryData.OverDays}</overDays>\r\n" +
                   $"      <month>{employee.SalaryData.Month}</month>\r\n" +
                   $"      <isPriv>{employee.SalaryData.IsPrivilegy}</isPriv>\r\n";
            return new WebServiceMethod("GetEmpSalary");
        }
    }
}
