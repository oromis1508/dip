namespace SalaryWebServiceEntities.Entities
{
    public class DBTable
    {
        private DBTable(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public static DBTable Employees => new DBTable("Employees");

        public static DBTable BaseSalaries => new DBTable("base_salaries");

        public static DBTable WorkDays => new DBTable("work_days");
    }
}

