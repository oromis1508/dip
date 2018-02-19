namespace demo.framework.Utils.Entities
{
    public class Employee
    {
        public int Id { get;}
        public string PrivateId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string MiddleName { get; }
        public int Experiense { get; }
        public int ProfessionId { get; }

        public SalaryData SalaryData { get; set; }

        public Employee(int id, string privateId, string firstName, string lastName, 
                        string middleName, int experiense, int professionId)
        {
            Id = id;
            PrivateId = privateId;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Experiense = experiense;
            ProfessionId = professionId;
        }
    }
}
