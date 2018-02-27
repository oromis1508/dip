namespace SalaryWebServiceEntities.Entities
{
    public class DatabaseTable
    {
        private readonly string _name;

        private DatabaseTable(string name)
        {
            _name = name;
        }

        public static DatabaseTable Employees => new DatabaseTable("employees");

        public static DatabaseTable WorkDays => new DatabaseTable("work_days");

        public static DatabaseTable BaseSalaries => new DatabaseTable("base_salaries");

        public override string ToString() => _name;
    }
}
